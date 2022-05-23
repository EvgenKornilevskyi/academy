using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Comments;

public class NonExistent : TestBase
{
    [Test]
    [Category("GetNonExistentComment")]
    [TestCaseSource(typeof(TestDataSourceGet), nameof(TestDataSourceGet.GetRequestReturnsStatusCodeNotFound))]
    
    public async Task GetNonExistentComment(TestData testData)
    {
        var responseCommentId =  testData.CommentRequest["GetRequest"].Id;

        var responseGet = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Comments +
                                                                  Endpoints.CommentId(responseCommentId) +
                                                                  Endpoints.AccessToken);

        Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["NotFound"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Comments}");

        await TestServices.HttpClientFactory.
            SendHttpRequestTo(HttpApisNames.Jsonplaceholder).
            Delete(Endpoints.Users + 
                   Endpoints.UserId(responseCommentId) + Endpoints.AccessToken);
    }

    private static class TestDataSourceGet
    {
        internal static IEnumerable GetRequestReturnsStatusCodeNotFound
        {
            get
            {
                var data = new TestData
                {
                    CommentRequest =
                    {
                        ["GetRequest"] = new Common.Configuration.Models.Comment
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