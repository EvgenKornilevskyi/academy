using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using Tests.Common.Configuration;

namespace Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestBase
    {
        protected HttpClient HttpClient;
        [SetUp]
        public void Init()
        {
            HttpClient = TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder);
        }

        [TearDown] 
        public void Cleanup()
        {
        }
    }
}