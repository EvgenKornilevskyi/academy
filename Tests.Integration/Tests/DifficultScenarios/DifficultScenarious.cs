using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.DifficultScenarios
{
    public class DifficultScenarious : TestBase
    {
        [Test]
        [Category("Difficult")]
        [TestCaseSource(typeof(DataSourceDifficultScenarious), nameof(DataSourceDifficultScenarious.ReturnsThreePosts))]
        public async Task GetRequest_GetThreePosts_ExpectedPostsReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequestList["PostRequestList"].ForEach(async post =>
            {
                post.UserId = User.Id;
                await IdentityCreator.CreateIdentity(Endpoints.Posts, post);
            });

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(User.Id) + Endpoints.Post
                 + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var postsFirstThree = JsonConvert.DeserializeObject<PostsResponse>(responseContent).Posts.Take(3).ToList();
            postsFirstThree.Reverse();

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            testData.PostRequestList["PostRequestList"].ForEach(async post =>
            {
                await IdentityCreator.DeleteIdentity(Endpoints.Posts, post);
            });

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Todos}");
                CollectionAssert.AreEqual(postsFirstThree, testData.PostRequestList["PostRequestList"],
                    "Actual first three posts arent equal to expected.");
            });
        }
        [Test]
        [Category("Difficult")]
        [TestCaseSource(typeof(DataSourceDifficultScenarious), nameof(DataSourceDifficultScenarious.ReturnsThreeComments))]
        public async Task GetRequest_GetThreeComments_ExpectedCommentsReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            testData.CommentRequestList["CommentRequestList"].ForEach(async comment =>
            {
                comment.PostId = Post.Id;
                await IdentityCreator.CreateIdentity(Endpoints.Comments, comment);
            });

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Posts + Endpoints.PostId(Post.Id) + Endpoints.Comment
                 + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var commentsFirstThree = JsonConvert.DeserializeObject<CommentsResponse>(responseContent).Comments.Take(3).ToList();
            commentsFirstThree.Reverse();

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);
            testData.CommentRequestList["CommentRequestList"].ForEach(async comment =>
            {
                await IdentityCreator.DeleteIdentity(Endpoints.Comments, comment);
            });

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Todos}");
                CollectionAssert.AreEqual(commentsFirstThree, testData.CommentRequestList["CommentRequestList"],
                    "Actual first three comments arent equal to expected.");
            });
        }
        [Test]
        [Ignore("Ignore a test")]
        [Category("Difficult")]
        [TestCaseSource(typeof(DataSourceDifficultScenarious), nameof(DataSourceDifficultScenarious.Returns21Posts))]
        public async Task GetRequest_Get21Post_ExpectedPostsReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequestList["PostRequestList"].ForEach(async post =>
            {
                post.UserId = User.Id;
                await IdentityCreator.CreateIdentity(Endpoints.Posts, post);
            });

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(User.Id) + Endpoints.Post
                 + Endpoints.Page(2) + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var postFirstFromSecondPage = JsonConvert.DeserializeObject<PostsResponse>(responseContent).Posts.ToList().First();

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            testData.PostRequestList["PostRequestList"].ForEach(async post =>
            {
                await IdentityCreator.DeleteIdentity(Endpoints.Posts, post);
            });

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Todos}");
                Assert.That(postFirstFromSecondPage, Is.EqualTo(testData.PostRequestList["PostRequestList"].First()),
                    "Actual first post on second page arent equal to expected.");
            });
        }
    }
}
