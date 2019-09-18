using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportLeagueAPI.Context;
using SportLeagueAPI.DTO;
using SportLeagueAPI.Services;

namespace SportLeagueAPI.Middleware
{
    public class GraphQLFileUploadMiddleware
    {
        private readonly RequestDelegate _next;
        private LeagueDbContext _dbContext;
        private MappedSchemaProvider<LeagueDbContext> _schemaProvider;

        public GraphQLFileUploadMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMediaUploader _mediaUploader)
        {
            if(!context.Request.HasFormContentType)
            {
                await _next(context);
                return;
            }
            //retrieve files
            var files = context.Request.Form.Files;
            //publish them and fetch their links
            var links = new List<string>();
            foreach(var file in files)
            {
                links.Add(await _mediaUploader.UploadMediaAsync(file));
            }
            
            _dbContext = context.RequestServices.GetRequiredService<LeagueDbContext>();
            _schemaProvider = context.RequestServices.GetRequiredService<MappedSchemaProvider<LeagueDbContext>>();

            //extract graphql with variables
            var forms = await context.Request.ReadFormAsync();            
            forms.TryGetValue("graphql", out var operationsJson);

            QueryRequest request = JsonConvert.DeserializeObject<QueryRequest>(operationsJson);
            if(request.Variables.Keys.Contains("links"))
            {
                var oldLinks = JsonConvert.DeserializeObject<List<string>>(request.Variables["links"].ToString());
                request.Variables.Remove("links");
                request.Variables.Add("links", links.Concat(oldLinks).ToList());
                foreach(string link in (List<string>)request.Variables["links"])
                    System.Console.Error.WriteLine(link);
            }
            else
            {
                request.Variables.Add("links", links.ToList());
            }
            
            var data = _dbContext.QueryObject(request, _schemaProvider);
            if(data.Errors != null && data.Errors.Count > 0)
                foreach(var error in data.Errors)
                    System.Console.Error.WriteLine(error.Message);
        }
    }
}