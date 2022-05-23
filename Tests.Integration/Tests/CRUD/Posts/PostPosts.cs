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
    public class PostPosts : TestBase
    {
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSourcePosts), nameof(TestDataSourcePosts.PostRequestReturnsStatusCodeCreated))]
        public async Task PostRequest_PostPost_ExpectedStatusCodeReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Posts + Endpoints.AccessToken,
                testData.PostRequest["PostRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var Post = JsonConvert.DeserializeObject<PostSingleResponse>(responseContent).Post;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Posts}");
        }
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSourcePosts), nameof(TestDataSourcePosts.PostRequestReturnsStatusCodeUnprocessableEntity))]
        public async Task PostRequest_PostInvalidPost_ExpectedStatusCodeReturned(TestData testData)
        {
            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Posts + Endpoints.AccessToken,
                testData.PostRequest["PostRequest"]);

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Posts}");
        }
    }
}

