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

namespace Tests.Integration.Tests.CRUD.Posts
{
    public class PutPosts : TestBase
    {
        [Test]
        [Category("Put")]
        [TestCaseSource(typeof(TestDataSourcePosts), nameof(TestDataSourcePosts.PutRequestUpdatesPostAllFields))]
        public async Task PutRequest_UpdatePostAllFields_ExpectedPostUpdated(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["initialPostRequest"].UserId = User.Id;
            testData.PostRequest["updatedPostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["initialPostRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Put(Endpoints.Posts + Endpoints.PostId(Post.Id)
                 + Endpoints.AccessToken, testData.PostRequest["updatedPostRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedPost = JsonConvert.DeserializeObject<PostSingleResponse>(responseContent).Post;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(updatedPost.UserId, Is.EqualTo(testData.PostRequest["updatedPostRequest"].UserId),
                    "Actual UserId isnt equal to expected.");
                Assert.That(updatedPost.Title, Is.EqualTo(testData.PostRequest["updatedPostRequest"].Title),
                    "Actual Title isnt equal to expected.");
                Assert.That(updatedPost.Body, Is.EqualTo(testData.PostRequest["updatedPostRequest"].Body),
                    "Actual Body isnt equal to expected.");
            });
        }
    }
}

