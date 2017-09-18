﻿using System;
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

namespace RDB.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("api", new Info
                {
                    Title = "RDB API",
                    Version = "0.0",
                    Description = "REST API to manage and query RDB"
                });

                var assembly = GetType().Assembly;
                var binPath = Path.GetDirectoryName(assembly.Location);
                var xmlPath = Path.Combine(binPath, $"{assembly.GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // mongodb
            var connectionString = _config.GetConnectionString("MongoDB");
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

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });

            var swaggerUiDir = Path.Combine(Directory.GetCurrentDirectory(), "node_modules/swagger-ui-dist");
            app.UseFileServer(new FileServerOptions
            {
                RequestPath = "/api",
                FileProvider = new SwaggerUiFileProvider(swaggerUiDir, new SwaggerUiOptions
                {
                    Url = "/api/swagger.json"
                })
            });

            app.UseMvc();
        }
    }
}
