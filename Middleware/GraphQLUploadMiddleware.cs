using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SportLeagueAPI.Context;

namespace SportLeagueAPI.Middleware
{
    public class GraphQLUploadMiddleware
    {
        private const string SpecUrl = "https://github.com/jaydenseric/graphql-multipart-request-spec";

        private readonly RequestDelegate _next;
        private LeagueDbContext _dbContext;
        private MappedSchemaProvider<LeagueDbContext> _schemaProvider;

        public GraphQLUploadMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!IsMultipartGraphQLRequest(context))
            {
                await _next.Invoke(context);
                return;
            }

            var forms = await context.Request.ReadFormAsync();

            if(!forms.TryGetValue("operations", out var operationsJson))
            {
                await WriteStatusCodeWithMessage(context, 400, $"Missing field 'operations' ({SpecUrl}).");
                return;
            }

            if(!forms.TryGetValue("map", out var mapJson))
            {
                await WriteStatusCodeWithMessage(context, 400, $"Missing field 'map' ({SpecUrl}).");
                return;
            }

            (var requests, var error) = ExtractGraphQLRequests(operationsJson, mapJson, forms);
            if(error != null)
            {
                await WriteStatusCodeWithMessage(context, 400, error);
                Console.Error.WriteLine(error);
                return;
            }
            _dbContext = context.RequestServices.GetRequiredService<LeagueDbContext>();
            _schemaProvider = context.RequestServices.GetRequiredService<MappedSchemaProvider<LeagueDbContext>>();
            List<QueryResult> results = new List<QueryResult>();
            foreach(var request in requests)
            {
                request.Request.Variables = (QueryVariables)request.GetInputs(); //<-

                var inputs = request.GetInputs();
                for(int i = 0;i < inputs.Count;i++)
                {
                    request.Request.Variables[inputs.ElementAt(i).Key] = inputs.ElementAt(i).Value;
                    System.Console.Error.WriteLine("X");
                }

                foreach(var entry in request.Request.Variables)
                {
                    System.Console.Error.WriteLine(entry.Key + " " + ((IFormFile)entry.Value).FileName);
                }

                var data = _dbContext.QueryObject(request.Request,_schemaProvider);
                System.Console.Error.WriteLine(data.Errors[0].Message);
                results.Add(data);
            }

            await WriteResponsesAsync(context, results.ToArray());
        }


        private static bool IsMultipartGraphQLRequest(HttpContext context)
        {
            return context.Request.HasFormContentType;
        }

        private static Task WriteStatusCodeWithMessage(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(message);
        }

        private async Task WriteResponsesAsync(HttpContext context, QueryResult[] results)
        {
            DocumentWriter writer = new DocumentWriter();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;

            if(results.Length == 1)
            {
                await writer.WriteAsync(context.Response.Body, results[0]);
                return;
            }

            using (var sw = new StreamWriter(context.Response.Body, Encoding.UTF8))
            {
                sw.AutoFlush = true;
                sw.Write("[");
                for (int i = 0; i <= results.Length - 2; i++)
                {
                    await writer.WriteAsync(context.Response.Body, results[i]);
                    sw.Write(",");
                }
                await writer.WriteAsync(context.Response.Body, results[results.Length - 1]);
                sw.Write("]");
            }
        }

        private static (List<NewQueryRequest> requests, string error) ExtractGraphQLRequests(string operationsJson, string mapJson, IFormCollection forms)
        {
            Dictionary<string, string[]> map;
            JToken operations;

            try
            {
                operations = JToken.Parse(operationsJson);
            }
            catch (JsonException)
            {
                return (null, $"Invalid JSON in the 'operations' multipart field ({SpecUrl}).");
            }

            try
            {
                map = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(mapJson);
            }
            catch (JsonException)
            {
                return (null, $"Invalid JSON in the 'map' multipart field ({SpecUrl}).");
            }

            List<NewQueryRequest> requests;
            var metaLookup = new Dictionary<int, List<Meta>>();

            foreach (var entry in map)
            {
                var file = forms.Files.GetFile(entry.Key);
                if(file is null)
                {
                    return (null, "File is null");
                }

                foreach (var path in entry.Value)
                {
                    (var index, var parts) = GetParts(path, operations is JArray);

                    if (!metaLookup.ContainsKey(index))
                    {
                        metaLookup.Add(index, new List<Meta>());
                    }

                    metaLookup[index].Add(new Meta { File = file, Parts = parts });
                }
            }

            if (operations is JArray)
            {
                int i = 0;
                requests = operations
                            .Select(j => CreateGraphQLRequest(j, metaLookup, i++))
                            .ToList();
            }
            else
            {
                var request = CreateGraphQLRequest(operations, metaLookup, 0);
                requests = new List<NewQueryRequest> { request };
            }

            return (requests, null);

        }

        private static NewQueryRequest CreateGraphQLRequest(JToken j, Dictionary<int, List<Meta>> metaLookup, int index)
        {
            var NewQueryRequest = new NewQueryRequest();
            NewQueryRequest.Request = j.ToObject<QueryRequest>();
            if (metaLookup.ContainsKey(index))
            {
                NewQueryRequest.TokensToReplace = metaLookup[index];
            }
            return NewQueryRequest;
        }

        private static (int requestIndex, List<object> parts) GetParts(string path, bool isBatchedRequest)
        {
            var results = new List<object>();
            var requestIndex = 0;

            foreach (var s in path.Split('.'))
            {
                if (int.TryParse(s, out int integer))
                    results.Add(integer);
                else
                    results.Add(s);
            }

            // remove request index and 'variables' part, 
            // because the parts list only needs to hold the parts relevant for each request
            // e.g: 0.variables.file.0 -> ["file", 0]

            if (isBatchedRequest)
            {
                requestIndex = (int)results[0];
                results.RemoveRange(0, 2);
            }
            else
            {
                results.RemoveAt(0);
            }

            return (requestIndex, results);
        }
    }

    public class Meta
    {
        public List<object> Parts { get; set; }

        public IFormFile File { get; set; }
    }
    public class DocumentWriter
    {
        private readonly JsonArrayPool<char> _jsonArrayPool = new JsonArrayPool<char>(ArrayPool<char>.Shared);
        private readonly JsonSerializer _serializer;
        internal static readonly Encoding Utf8Encoding = new UTF8Encoding(false);

        public DocumentWriter()
            : this(indent: false)
        {
        }

        public DocumentWriter(bool indent)
            : this(
                indent ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings())
        {
        }

        public DocumentWriter(Formatting formatting, JsonSerializerSettings settings)
        {
            _serializer = JsonSerializer.CreateDefault(settings);
            _serializer.Formatting = formatting;
        }

        public async Task WriteAsync<T>(Stream stream, T value)
        {
            using (var writer = new HttpResponseStreamWriter(stream, Utf8Encoding))
            using (var jsonWriter = new JsonTextWriter(writer)
            {
                ArrayPool = _jsonArrayPool,
                CloseOutput = false,
                AutoCompleteOnClose = false
            })
            {
                _serializer.Serialize(jsonWriter, value);
                await jsonWriter.FlushAsync().ConfigureAwait(false);
            }
        }

        public string Write(object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (!(value is QueryResult))
            {
                throw new ArgumentOutOfRangeException($"Expected {nameof(value)} to be a GraphQL.ExecutionResult, got {value.GetType().FullName}");
            }

            return this.WriteToStringAsync(value).GetAwaiter().GetResult();
        }
    }

    public static class DocumentWriterExtensions
    {
        /// <summary>
        /// Writes the <paramref name="value"/> to string.
        /// </summary>
        public static async Task<string> WriteToStringAsync<T>(this DocumentWriter writer, T value)
        {
            using (var stream = new MemoryStream())
            {
                await writer.WriteAsync(stream, value).ConfigureAwait(false);
                stream.Position = 0;
                using (var reader = new StreamReader(stream, DocumentWriter.Utf8Encoding))
                {
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }
    }
}