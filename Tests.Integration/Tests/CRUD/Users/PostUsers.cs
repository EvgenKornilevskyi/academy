using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests
{
    public class PostUsers : TestBase
    {
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSource), nameof(TestDataSource.PostsRequestReturnsStatusCodeCreated))]
        public async Task PostsRequest_SendPostRequest_ExpectedStatusCodeReturned(TestData testData)
        {
            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken, testData.PostRequest["PostsRequest"]);

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Posts}");
        }
    }

    internal static class TestDataSource
    {
        internal static IEnumerable PostsRequestReturnsStatusCodeCreated
        {
            get
            {
                var data = new TestData();

                data.PostRequest["PostsRequest"] = new User();

                data.PostRequest["PostsRequest"].Id = TestServices.Rand;
                data.PostRequest["PostsRequest"].Name = "Oleg";
                data.PostRequest["PostsRequest"].Email = "Oleg@mail.com";
                data.PostRequest["PostsRequest"].Gender = "male";
                data.PostRequest["PostsRequest"].Status = "active";

                data.StatusCode["StatusCode"] = HttpStatusCode.Created;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn201");
            }
        }
    }
}

