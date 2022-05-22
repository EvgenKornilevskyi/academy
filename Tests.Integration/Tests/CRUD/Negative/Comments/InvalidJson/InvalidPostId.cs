using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Comments.InvalidJson;

public class InvalidPostId
{
    [Test]
    [Category("PostCommentInvalidPostId")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnprocessableEntity))]
    public async Task PostRequestInvalidPostId(TestData testData)
    {
        var comment  = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Comments + Endpoints.AccessToken,
                testData.CommentRequest["PostRequest"]);

        Assert.That(comment.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Comments}");
    }


    private static class TestDataSourcePost
    {
        internal static IEnumerable PostRequestReturnsStatusCodeUnprocessableEntity
        {
            get
            {
                var data = new TestData
                {

                    CommentRequest =
                    {
                        ["PostRequest"] = new Comment()
                        {
                            Body = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Name = TestServices.NewId,
                            PostId = -1
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