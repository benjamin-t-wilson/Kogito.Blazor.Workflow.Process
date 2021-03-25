using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace Kogito.Blazor.Workflow.Process.Factories
{
    public class HttpClientFactory : Interfaces.IHttpClientFactory
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;

        public HttpClientFactory(IConfiguration config)
        {
            _config = config;
            _baseUrl = _config.GetValue<string>("Kogito:BaseUrl");
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };

            return client;
        }
    }
}