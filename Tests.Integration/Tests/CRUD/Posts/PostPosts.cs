using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.Posts
{
    public class PostPosts : TestBase
    {
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeCreated))]
        public async Task PostRequest_PostPost_ExpectedStatusCodeReturned(TestData testData)
        {
            var user = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Posts + Endpoints.AccessToken,
                testData.PostRequest["PostRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var Post = JsonConvert.DeserializeObject<PostSingleResponse>(responseContent).Post;

            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
        }
    }

    internal static class TestDataSourcePost
    {
        internal static IEnumerable PostRequestReturnsStatusCodeCreated
        {
            get
            {
                var data = new TestData();

                data.PostRequest["PostRequest"] = new Post();

                data.PostRequest["PostRequest"].Id = 1;
                data.PostRequest["PostRequest"].UserId = 2;
                data.PostRequest["PostRequest"].Title = "Some story";
                data.PostRequest["PostRequest"].Body = "Here goes story";

                data.StatusCode["StatusCode"] = HttpStatusCode.Created;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn201");
            }
        }
    }
}

