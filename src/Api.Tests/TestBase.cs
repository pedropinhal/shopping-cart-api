using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Api.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Api.Concrete;

namespace Api.Tests
{
    public class TestBase : IDisposable
    {
        protected readonly TestServer _api;
        protected readonly HttpClient _client;

        public TestBase()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _api = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {
                    
                })
                .UseConfiguration(configuration));

            _client = _api.CreateClient();
        }

        public void Dispose()
        {
            _api.Dispose();
            _client.Dispose();
        }

        protected StringContent Serialize(object value)
        {
            var json = JsonConvert.SerializeObject(value);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var model = !string.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<T>(content) : default(T);

            return model;
        }
    }
}