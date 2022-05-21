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

namespace Tests.Integration.Tests.CRUD.Comments
{
    public class GetComments : TestBase
    {
        [Test]
        [Category("Get")]
        [TestCaseSource(typeof(TestDataSourceComments), nameof(TestDataSourceComments.GetRequestReturnsComment))]
        public async Task GetRequest_GetComment_ExpectedCommentReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            testData.CommentRequest["CommentRequest"].PostId = Post.Id;

            var Comment = await IdentityCreator.CreateIdentity(Endpoints.Comments, testData.CommentRequest["CommentRequest"]);

            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Comments + Endpoints.CommentId(Comment.Id) + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseComment = JsonConvert.DeserializeObject<CommentSingleResponse>(responseContent).Comment;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);
            await IdentityCreator.DeleteIdentity(Endpoints.Comments, Comment);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Comments}");
                Assert.That(responseComment.Id, Is.EqualTo(Comment.Id),
                    "Actual Id isnt equal to expected.");
                Assert.That(responseComment.PostId, Is.EqualTo(testData.CommentRequest["CommentRequest"].PostId),
                    "Actual Name isnt equal to expected.");
                Assert.That(responseComment.Name, Is.EqualTo(testData.CommentRequest["CommentRequest"].Name),
                    "Actual Email isnt equal to expected.");
                Assert.That(responseComment.Email, Is.EqualTo(testData.CommentRequest["CommentRequest"].Email),
                    "Actual Gender isnt equal to expected.");
                Assert.That(responseComment.Body, Is.EqualTo(testData.CommentRequest["CommentRequest"].Body),
                    "Actual Gender isnt equal to expected.");
            });
        }
    }
}



