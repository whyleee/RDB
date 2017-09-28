using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RDB.Api.Business;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.FileProviders;
using RDB.Api.Business.Swagger;

namespace RDB.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public static string MongoDbConnectionString { get; set; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0", new Info
                {
                    Title = "Rdb Api",
                    Version = "0.0",
                    Description = "REST API to manage and query RDB."
                });

                var assembly = GetType().Assembly;
                var binPath = Path.GetDirectoryName(assembly.Location);
                var xmlPath = Path.Combine(binPath, $"{assembly.GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);

                c.DocumentFilter<LowercasePathsDocumentFilter>();
            });

            // mongodb
            var connectionString = MongoDbConnectionString ?? _config.GetConnectionString("MongoDB");
            var mongoUrl = new MongoUrl(connectionString);
            services.AddSingleton<IMongoClient>(s => new MongoClient(mongoUrl));
            services.AddSingleton<IMongoDatabase>(s => s.GetService<IMongoClient>().GetDatabase(mongoUrl.DatabaseName));
            
            // app
            services.AddSingleton<ValueStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/api");
            app.UseMvc();

            if (env.IsEnvironment("Test"))
            {
                return;
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });

            var currentDir = Directory.GetCurrentDirectory();
            var staticDir = Path.Combine(currentDir, "wwwroot");
            var nodeModulesDir = Path.Combine(currentDir, "node_modules");
            var swaggerUiDir = Path.Combine(nodeModulesDir, "swagger-ui-dist");
            var swaggerUiThemesDir = Path.Combine(nodeModulesDir, "swagger-ui-themes/themes/3.x");

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = "/static",
                FileProvider = new PhysicalFileProvider(staticDir)
            });
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = "/swagger-ui-themes",
                FileProvider = new PhysicalFileProvider(swaggerUiThemesDir)
            });
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = "",
                FileProvider = new SwaggerUiFileProvider(swaggerUiDir, new SwaggerUiOptions
                {
                    Title = "RDB API",
                    Url = "/api/v0/swagger.json",
                    CustomCssUrls = new[]
                    {
                        "/api/swagger-ui-themes/theme-material.css",
                        "/api/static/swagger-ui-custom.css"
                    },
                    FaviconUrl = "/api/static/favicon.ico"
                })
            });
        }
    }
}
