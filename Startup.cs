using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using SportLeagueAPI.Context;
using SportLeagueAPI.GraphQL;
using SportLeagueAPI.Middleware;
using SportLeagueAPI.Repositories;
using SportLeagueAPI.Services;

namespace SportLeagueAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("EnableCors", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddHttpContextAccessor();
            services.AddDbContext<LeagueDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Database")));

            services.AddTransient<IHasher,FileNameHasher>();
            services.AddTransient<IPathsProvider,PathsProvider>();
            services.AddTransient<IMediaUploader,MediaUploader>();

            services.AddSingleton(AppSchema.MakeSchema());
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IPathsProvider pathsProvider)
        {
            app.UseCors("EnableCors");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware<FileUploadMiddleware>();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(pathsProvider.MediaPath),
                RequestPath = "/media"
            });
            app.UseMvc();
        }
    }
}