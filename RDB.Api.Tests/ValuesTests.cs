using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Mongo2Go;
using RDB.Models;
using Xunit;

namespace RDB.Api.Tests
{
    public class ValuesTests : IAsyncLifetime, IDisposable
    {
        private readonly MongoDbRunner _dbRunner;
        private readonly TestServer _server;
        private readonly IRdbApi _api;

        private readonly Dictionary<string, string> _valueIds = new Dictionary<string, string>();

        public ValuesTests()
        {
            _dbRunner = MongoDbRunner.Start(Path.GetTempPath());
            Startup.MongoDbConnectionString = _dbRunner.ConnectionString + "rdb";

            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>());

            var client = _server.CreateClient();
            _api = new RdbApi(client);
        }

        public async Task InitializeAsync()
        {
            _valueIds.Add("one", ((Value) await _api.ValuesPostAsync("one")).Id);
            _valueIds.Add("two", ((Value) await _api.ValuesPostAsync("two")).Id);
            _valueIds.Add("three", ((Value) await _api.ValuesPostAsync("three")).Id);
        }

        [Fact]
        public async Task GetAll()
        {
            var values = await _api.ValuesGetAsync();

            Assert.Equal(3, values.Count);
            Assert.All(values, val => Assert.True(_valueIds.ContainsValue(val.Id)));
            Assert.All(values, val => Assert.True(_valueIds.ContainsKey(val.Text)));
        }

        public void Dispose()
        {
            _dbRunner.Dispose();
            _server.Dispose();
            _api.Dispose();
        }

        public Task DisposeAsync()
        {
            return Task.FromResult(0);
        }
    }
}
