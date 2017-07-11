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

namespace RDB.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // mongodb
            var connectionString = "mongodb://mongo:27017/rdb";
            var mongoUrl = new MongoUrl(connectionString);
            services.AddSingleton<IMongoClient>(sl => new MongoClient(mongoUrl));
            services.AddSingleton<IMongoDatabase>(sl => sl.GetService<IMongoClient>().GetDatabase(mongoUrl.DatabaseName));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
