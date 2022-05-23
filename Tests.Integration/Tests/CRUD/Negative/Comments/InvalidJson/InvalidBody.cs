using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Comments.InvalidJson;

public class InvalidBody
{
    [Test]
    [Category("PostCommentInvalidBody")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnprocessableEntity))]
    public async Task PostRequestInvalidBody(TestData testData)
    {
        var user = await IdentityCreator.CreateIdentity(Endpoints.Users,
            testData.UserRequest["PostRequest"]);

        testData.PostRequest["PostRequest"].UserId = user.Id;

        var post = await IdentityCreator.CreateIdentity(Endpoints.Posts,
            testData.PostRequest["PostRequest"]);

        testData.CommentRequest["PostRequest"].PostId = post.Id;
        
        var comment  = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Comments + Endpoints.AccessToken,
                testData.CommentRequest["PostRequest"]);

        Assert.That(comment.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Comments}");
        
        await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
        await IdentityCreator.DeleteIdentity(Endpoints.Posts, post);
    }


    private static class TestDataSourcePost
    {
        internal static IEnumerable PostRequestReturnsStatusCodeUnprocessableEntity
        {
            get
            {
                var data = new TestData
                {
                    UserRequest =
                    {
                        ["PostRequest"] = new User
                        {
                            Name = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Gender = "male",
                            Status = "active"
                        }
                    },

                    PostRequest =
                    {
                        ["PostRequest"] = new Post
                        {
                            Body = TestServices.NewId,
                            Title = TestServices.NewId
                        }
                    },
                    
                    CommentRequest =
                    {
                        ["PostRequest"] = new Comment()
                        {
                            Body = "",
                            Email = TestServices.NewId + "@mail.com",
                            Name = TestServices.NewId,
                        }
                    },

                    StatusCode =
                    {
                        ["StatusCode"] = HttpStatusCode.UnprocessableEntity
                    }
                };

                yield return new TestCaseData(data).SetArgDisplayNames("UnprocessableEntity422");
            }
        }
    }
}