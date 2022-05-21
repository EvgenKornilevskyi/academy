using Newtonsoft.Json;
using NUnit.Framework;
using Polly;
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
    public class PostComments : TestBase
    {
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSourceComments), nameof(TestDataSourceComments.PostCommentRequestReturnsStatusCodeCreated))]
        public async Task PostRequest_PostComment_ExpectedStatusCodeReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);
            
            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            testData.CommentRequest["CommentRequest"].PostId = Post.Id;

            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Comments + Endpoints.AccessToken,
                testData.CommentRequest["CommentRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var Comment = JsonConvert.DeserializeObject<CommentSingleResponse>(responseContent).Comment;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            await IdentityCreator.DeleteIdentity(Endpoints.Posts, Post);
            await IdentityCreator.DeleteIdentity(Endpoints.Comments, Comment);

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Comments}");
        }
    }
}


