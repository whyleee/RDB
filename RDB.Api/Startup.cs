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
            services.AddMvc();

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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
