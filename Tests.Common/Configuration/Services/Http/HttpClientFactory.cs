using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using ResultsManager.Tests.Common.Configuration.Options;

namespace ResultsManager.Tests.Common.Configuration.Services.Http
{
    public static class HttpApisNames
    {
        public const string Jsonplaceholder = "Jsonplaceholder";
    }

    public class HttpClientFactory
    {
        private readonly ConcurrentDictionary<string, HttpClient> _httpApis = new ConcurrentDictionary<string, HttpClient>();

        public HttpClientFactory(IOptions<TestServicesOptions> options)
        {
            _httpApis[HttpApisNames.Jsonplaceholder] = new HttpClient { BaseAddress = new Uri(options.Value.HttpLinksOptions.Jsonplaceholder) };    
        }

        public HttpClient SendHttpRequestTo(string name) => _httpApis[name];
    }
}