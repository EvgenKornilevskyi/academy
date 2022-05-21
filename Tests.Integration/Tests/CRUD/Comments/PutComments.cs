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

namespace Tests.Integration.Tests.CRUD.Comments
{
    public class PutComments : TestBase
    {
        [Test]
        [Category("Put")]
        [TestCaseSource(typeof(TestDataSourceComments), nameof(TestDataSourceComments.PutRequestUpdatesComment))]
        public async Task PutRequest_UpdateComment_ExpectedCommentUpdated(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            testData.CommentRequest["initialCommentRequest"].PostId = Post.Id;
            testData.CommentRequest["updatedCommentRequest"].PostId = Post.Id;

            var Comment = await IdentityCreator.CreateIdentity(Endpoints.Comments, testData.CommentRequest["initialCommentRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Put(Endpoints.Comments + Endpoints.CommentId(Comment.Id)
                 + Endpoints.AccessToken, testData.CommentRequest["updatedCommentRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedComment = JsonConvert.DeserializeObject<CommentSingleResponse>(responseContent).Comment;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Comment);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Comments}");
                Assert.That(updatedComment.Id, Is.EqualTo(Comment.Id),
                    "Actual Email isnt equal to expected.");
                Assert.That(updatedComment.PostId, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].PostId),
                    "Actual Gender isnt equal to expected.");
                Assert.That(updatedComment.Name, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].Name),
                    "Actual Status isnt equal to expected.");
                Assert.That(updatedComment.Email, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].Email),
                    "Actual Status isnt equal to expected.");
                Assert.That(updatedComment.Body, Is.EqualTo(testData.CommentRequest["updatedCommentRequest"].Body),
                    "Actual Status isnt equal to expected.");
            });
        }
    }
}


