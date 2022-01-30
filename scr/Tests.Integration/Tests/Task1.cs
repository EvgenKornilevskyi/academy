using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Domain;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests
{
    public class Task1 : TestBase
    {
        [TestCaseSource(typeof(TestDataSource1), nameof(TestDataSource1.PostsRequestData))]
        public async Task HwTask1(TestData testData)
        {
            //var response = await TestServices.HttpClientFactory
            //    .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Posts, postsRequestObject);

            //Assert place
        }
    }

    internal static class TestDataSource1
    {
        internal static IEnumerable PostsRequestData
        {
            get
            {
                var data = new TestData();
                data.PostsRequest["PostsRequest"] = new PostsRequest();
                yield return new TestCaseData(data).SetArgDisplayNames("PostsRequest");
            }
        }
    }
}
