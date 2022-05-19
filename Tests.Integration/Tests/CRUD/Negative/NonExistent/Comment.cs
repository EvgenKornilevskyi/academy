using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.NonExistent;

public class Comment
{
    [Test]
    [Category("GetNonExistentPost")]
    [TestCaseSource(typeof(TestDataSourceGet), nameof(TestDataSourceGet.GetRequestReturnsPost))]
    
    public async Task GetNonExistentPost(TestData testData)
    {
        var responseCommentId =  testData.CommentRequest["GetRequest"].Id;

        var responseGet = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Comments +
                                                                  Endpoints.CommentId(responseCommentId) +
                                                                  Endpoints.AccessToken);

        Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["NotFound"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");

        await TestServices.HttpClientFactory.
            SendHttpRequestTo(HttpApisNames.Jsonplaceholder).
            Delete(Endpoints.Users + 
                   Endpoints.UserId(responseCommentId) + Endpoints.AccessToken);
    }

    internal static class TestDataSourceGet
    {
        internal static IEnumerable GetRequestReturnsPost
        {
            get
            {
                var data = new TestData();

                data.CommentRequest["GetRequest"] = new Common.Configuration.Models.Comment();

                data.CommentRequest["GetRequest"].Id = Convert.ToInt32(-1);

                data.StatusCode["NotFound"] = HttpStatusCode.NotFound;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ReturnsCommentById");
            }
        }
    }
}