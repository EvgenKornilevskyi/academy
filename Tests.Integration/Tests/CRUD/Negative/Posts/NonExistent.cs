using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;
using Post = Tests.Integration.Tests.CRUD.Negative.Comments.WithoutToken.Post;

namespace Tests.Integration.Tests.CRUD.Negative.Posts;

public class NonExistent : TestBase
{
    [Test]
    [Category("GetNonExistentPost")]
    [TestCaseSource(typeof(TestDataSourceGet), nameof(TestDataSourceGet.GetRequestReturnsStatusCodeNotFound))]
    
    public async Task GetNonExistentPost(TestData testData)
    {
        var responsePostId =  testData.PostRequest["GetRequest"].Id;

        var responseGet = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Posts +
                                                                  Endpoints.PostId(responsePostId) +
                                                                  Endpoints.AccessToken);

        Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["NotFound"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Posts}");

        await TestServices.HttpClientFactory.
            SendHttpRequestTo(HttpApisNames.Jsonplaceholder).
            Delete(Endpoints.Users + 
                   Endpoints.UserId(responsePostId) + Endpoints.AccessToken);
    }

    private static class TestDataSourceGet
    {
        internal static IEnumerable GetRequestReturnsStatusCodeNotFound
        {
            get
            {
                var data = new TestData
                {
                    PostRequest =
                    {
                        ["GetRequest"] = new Common.Configuration.Models.Post
                        {
                            Id = Convert.ToInt32(-1)
                        }
                    },
                    StatusCode =
                    {
                        ["NotFound"] = HttpStatusCode.NotFound
                    }
                };

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("NotFoundReturn404");
            }
        }
    }
}