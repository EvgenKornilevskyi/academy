using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Posts.InvalidJson;

public class InvalidUserId : TestBase
{
    [Test]
    [Category("PostPostInvalidUserId")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnprocessableEntity))]
    public async Task PostPostInvalidUserId(TestData testData)
    {

        var response = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Posts + Endpoints.AccessToken,
                testData.PostRequest["PostRequest"]);

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Posts}");
    }


    private static class TestDataSourcePost
    {
        internal static IEnumerable PostRequestReturnsStatusCodeUnprocessableEntity
        {
            get
            {
                var data = new TestData
                {
                    PostRequest =
                    {
                        ["PostRequest"] = new Post
                        {
                            Body = TestServices.NewId,
                            Title = TestServices.NewId,
                            UserId = -1
                        }
                    },

                    StatusCode =
                    {
                        ["StatusCode"] = HttpStatusCode.UnprocessableEntity
                    }
                };

                yield return new TestCaseData(data).SetArgDisplayNames("UnprocessableEntity422");
            }
        }
    }
}