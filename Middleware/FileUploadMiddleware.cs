using System.Collections.Generic;
using System.Threading.Tasks;
using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SportLeagueAPI.Context;
using SportLeagueAPI.Services;

namespace SportLeagueAPI.Middleware
{
    public class FileUploadMiddleware
    {
        private readonly RequestDelegate _next;
        private LeagueDbContext _dbContext;
        private MappedSchemaProvider<LeagueDbContext> _schemaProvider;
        IMediaUploader _mediaUploader;

        public FileUploadMiddleware(RequestDelegate next, IMediaUploader mediaUploader)
        {
            _next = next;
            _mediaUploader = mediaUploader;
        }

        public async Task InvokeAsync(HttpContext context)
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
                links.Add(_mediaUploader.UploadMedia(file));
            }
            
            if(_dbContext == null)
            {
                _dbContext = context.RequestServices.GetRequiredService<LeagueDbContext>();
                _schemaProvider = context.RequestServices.GetRequiredService<MappedSchemaProvider<LeagueDbContext>>();
            }
            var forms = await context.Request.ReadFormAsync();

            QueryRequest request = new QueryRequest();
            
            forms.TryGetValue("operations", out var operationsJson);
            request = JsonConvert.DeserializeObject<QueryRequest>(operationsJson);

            if(request.Variables == null)
            {
                request.Variables = new QueryVariables();
            }

            if(links.Count == 1)
            {
                request.Variables.Add("link", links[0]);
            }
            else if(links.Count > 1)
            {
                request.Variables.Add("link", links.ToArray());                
            }

            var data = _dbContext.QueryObject(request, _schemaProvider);
            if(data.Errors != null && data.Errors.Count > 0)
            {
                foreach(var error in data.Errors)
                    System.Console.Error.WriteLine(error.Message);
            }
        }
    }
}