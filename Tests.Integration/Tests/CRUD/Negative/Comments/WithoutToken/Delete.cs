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

    public class Delete : TestBase
{
    [Test]
    [Category("DeleteCommentWithoutToken")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.DeleteRequestReturnsStatusCodeNotFound))]
    public async Task DeleteRequestWithoutToken(TestData testData)
    {
        var user = await IdentityCreator.CreateIdentity(Endpoints.Users,
            testData.UserRequest["DeleteRequest"]);

        testData.PostRequest["DeleteRequest"].UserId = user.Id;

        var post = await IdentityCreator.CreateIdentity(Endpoints.Posts,
            testData.PostRequest["DeleteRequest"]);

        testData.CommentRequest["DeleteRequest"].PostId = post.Id;
        
        var comment = await IdentityCreator.CreateIdentity(Endpoints.Comments, 
            testData.CommentRequest["DeleteRequest"]);
        
        var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Comments + 
                                                                         Endpoints.CommentId(comment.Id));
            

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Posts}");

        await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
        await IdentityCreator.DeleteIdentity(Endpoints.Posts, post);
    }


    private static class TestDataSourcePost
    {
        internal static IEnumerable DeleteRequestReturnsStatusCodeNotFound
        {
            get
            {
                var data = new TestData
                {
                    UserRequest =
                    {
                        ["DeleteRequest"] = new User
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
                        ["DeleteRequest"] = new Common.Configuration.Models.Post
                        {
                            Body = TestServices.NewId,
                            Id = 2,
                            Title = TestServices.NewId
                        },
                    },
                    
                    CommentRequest =
                    {
                        ["DeleteRequest"] = new Comment()
                        {
                            Body = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Id = TestServices.Rand,
                            Name = TestServices.NewId
                        },
                    },

                    StatusCode =
                    {
                        ["StatusCode"] = HttpStatusCode.NotFound
                    }
                };

                yield return new TestCaseData(data).SetArgDisplayNames("NotFoundReturn404");
            }
        }
    }
}