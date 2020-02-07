using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using SportLeagueAPI.Context;
using SportLeagueAPI.Models.Options;
using SportLeagueAPI.Repositories;
using SportLeagueAPI.Services;
using GraphiQl;
using GraphQL.Server;
using GraphQL;
using SportLeagueAPI.GraphQL;
using GraphQL.Types;
using SportLeagueAPI.GraphQL.Types;

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

            var jwtSection = Configuration.GetSection("Jwt");
            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);
            services.Configure<JwtOptions>(jwtSection);

            services.AddTransient<IJwtHandler,JwtHandler>();
            services.AddTransient<IAuthService,AuthService>();

            services.AddHttpContextAccessor();
            services.AddDbContext<LeagueDbContext>(options => 
            {
                options.UseSqlite(Configuration.GetConnectionString("Database"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg => 
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });

                
            services.AddTransient<IHasher,FileNameHasher>();
            services.AddTransient<IPathsProvider,PathsProvider>();
            services.AddTransient<IMediaUploader,MediaUploader>();

            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter,DocumentExecuter>();
            services.AddSingleton<MediaType>();
            services.AddSingleton<PlayerType>();
            services.AddSingleton<SettlementType>();
            services.AddSingleton<LeagueQuery>();
            
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new LeagueSchema(new FuncDependencyResolver(type => sp.GetService(type))));

            services.AddGraphQL();
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
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(pathsProvider.MediaPath),
                RequestPath = "/media"
            });
            app.UseGraphiQl("/graphiql","/graphql");
            app.UseMvc();
        }
    }
}