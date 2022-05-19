using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.NonExistent;

public class Posts
{
    [Test]
    [Category("GetNonExistentPost")]
    [TestCaseSource(typeof(TestDataSourceGet), nameof(TestDataSourceGet.GetRequestReturnsPost))]
    
    public async Task GetNonExistentPost(TestData testData)
    {
        var responsePostId =  testData.PostRequest["GetRequest"].Id;

        var responseGet = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Posts +
                                                                  Endpoints.PostId(responsePostId) +
                                                                  Endpoints.AccessToken);

        Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["NotFound"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");

        await TestServices.HttpClientFactory.
            SendHttpRequestTo(HttpApisNames.Jsonplaceholder).
            Delete(Endpoints.Users + 
                   Endpoints.UserId(responsePostId) + Endpoints.AccessToken);
    }

    internal static class TestDataSourceGet
    {
        internal static IEnumerable GetRequestReturnsPost
        {
            get
            {
                var data = new TestData();

                data.PostRequest["GetRequest"] = new Post();

                data.PostRequest["GetRequest"].Id = Convert.ToInt32(-1);

                data.StatusCode["NotFound"] = HttpStatusCode.NotFound;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ReturnsPostById");
            }
        }
    }
}