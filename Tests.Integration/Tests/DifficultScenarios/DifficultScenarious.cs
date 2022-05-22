using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
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
                 + Endpoints.Page(1) + Endpoints.AccessToken);
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
        [Test]
        [Category("Difficult")]
        [TestCaseSource(typeof(DataSourceDifficultScenarious), nameof(DataSourceDifficultScenarious.ReturnsUpdatedComment))]
        public async Task GetRequest_GetUpdatedComment_ExpectedCommentReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            testData.CommentRequest["initialCommentRequest"].PostId = Post.Id;
            testData.CommentRequest["updatedCommentRequest"].PostId = Post.Id;

            var Comment = await IdentityCreator.CreateIdentity(Endpoints.Comments, testData.CommentRequest["initialCommentRequest"]);

            var rsp = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Put(Endpoints.Comments + Endpoints.CommentId(Comment.Id)  + Endpoints.AccessToken, 
                                                                        testData.CommentRequest["updatedCommentRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Posts + Endpoints.PostId(Post.Id) + Endpoints.Comment
                 + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedComment = JsonConvert.DeserializeObject<CommentsResponse>(responseContent).Comments.FirstOrDefault();

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);
            await IdentityCreator.DeleteIdentity(Endpoints.Comments, Comment);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Comments}");
                Assert.That(updatedComment.Id, Is.EqualTo(Comment.Id),
                    "Actual Id isnt equal to expected.");
                Assert.That(updatedComment.PostId, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].PostId),
                    "Actual PostId isnt equal to expected.");
                Assert.That(updatedComment.Name, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(updatedComment.Email, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(updatedComment.Body, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].Body),
                    "Actual Body isnt equal to expected.");
            });
        }

        [Test]
        [Category("Difficult")]
        [TestCaseSource(typeof(DataSourceDifficultScenarious), nameof(DataSourceDifficultScenarious.ReturnsNonExistentPost))]
        public async Task Get_CommentOfNonExistentPost_CurrentNull(TestData testData)
        {
            var post = testData.PostRequest["PostRequest"];

            //Arrange
            var response = await TestServices.HttpClientFactory.SendHttpRequestTo(HttpApisNames.Jsonplaceholder)
                .Get($"{Endpoints.Posts}/{post.Id}{Endpoints.Comment}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<CommentsResponse>(responseBody);

            //Act
            var actualMetaCurrent = entities.Meta.Pagination.Links.Current;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                    $"Actual StatusCode isn't equal to expected.");
                Assert.That(actualMetaCurrent, Is.Null,
                    "Actual Meta Current parameter is not null.");
            });
        }
    }
}
