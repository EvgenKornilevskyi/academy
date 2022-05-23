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
using Tests.Integration.Tests.CRUD.Posts;

namespace Tests.Integration.Tests.CRUD.Posts
{
    public class GetPosts : TestBase
    {
        [Test]
        [Category("Get")]
        [TestCaseSource(typeof(TestDataSourcePosts), nameof(TestDataSourcePosts.GetRequestReturnsPost))]
        public async Task GetRequest_GetPost_ExpectedPostReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Posts + Endpoints.PostId(Post.Id) + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responsePost = JsonConvert.DeserializeObject<PostSingleResponse>(responseContent).Post;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(responsePost.Id, Is.EqualTo(Post.Id),
                    "Actual Id isnt equal to expected.");
                Assert.That(responsePost.UserId, Is.EqualTo(testData.PostRequest["PostRequest"].UserId),
                    "Actual UserId isnt equal to expected.");
                Assert.That(responsePost.Title, Is.EqualTo(testData.PostRequest["PostRequest"].Title),
                    "Actual Title isnt equal to expected.");
                Assert.That(responsePost.Body, Is.EqualTo(testData.PostRequest["PostRequest"].Body),
                    "Actual Body isnt equal to expected.");
            });
        }
    }
}


