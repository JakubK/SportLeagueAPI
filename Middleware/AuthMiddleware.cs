using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportLeagueAPI.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                var body = reader.ReadToEnd();
                if(body.Contains("mutation"))
                {
                    if(context.User.Identity.IsAuthenticated)
                    {
                        await _next(context);
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = 401; //UnAuthorized
                        await context.Response.WriteAsync("Invalid or Missing User Key");
                        return;
                    }
                }
            }
        }
    }
}