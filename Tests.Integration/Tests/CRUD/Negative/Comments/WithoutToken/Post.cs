using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Comments.WithoutToken;

public class Post : TestBase
{
    [Test]
    [Category("PostCommentWithoutToken")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnauthorized))]
    public async Task PostRequestWithoutToken(TestData testData)
    {
        var user = await IdentityCreator.CreateIdentity(Endpoints.Users,
            testData.UserRequest["PostRequest"]);

        testData.PostRequest["PostRequest"].UserId = user.Id;

        var post = await IdentityCreator.CreateIdentity(Endpoints.Posts,
            testData.PostRequest["PostRequest"]);

        testData.CommentRequest["PostRequest"].PostId = post.Id;
        
        var comment  = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Comments,
                testData.CommentRequest["PostRequest"]);

        Assert.That(comment.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Posts}");
        
        await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
        await IdentityCreator.DeleteIdentity(Endpoints.Posts, post);
    }


    private static class TestDataSourcePost
    {
        internal static IEnumerable PostRequestReturnsStatusCodeUnauthorized
        {
            get
            {
                var data = new TestData
                {
                    UserRequest =
                    {
                        ["PostRequest"] = new User
                        {
                            Id = TestServices.Rand,
                            Name = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Gender = "male",
                            Status = "active"
                        }
                    },

                    PostRequest =
                    {
                        ["PostRequest"] = new Common.Configuration.Models.Post
                        {
                            Body = TestServices.NewId,
                            Id = 1,
                            Title = TestServices.NewId
                        }
                    },
                    
                    CommentRequest =
                    {
                        ["PostRequest"] = new Common.Configuration.Models.Comment()
                        {
                            Body = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Id = TestServices.Rand,
                            Name = TestServices.NewId
                        }
                    },

                    StatusCode =
                    {
                        ["StatusCode"] = HttpStatusCode.Unauthorized
                    }
                };

                yield return new TestCaseData(data).SetArgDisplayNames("UnauthorizedReturn404");
            }
        }
    }
}