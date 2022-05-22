using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Posts.InvalidJson;

public class InvalidBody
{
    [Test]
    [Category("PostPostInvalidBody")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnprocessableEntity))]
    public async Task PostPostInvalidBody(TestData testData)
    {
        var user = await IdentityCreator.CreateIdentity(Endpoints.Users,
            testData.UserRequest["PostRequest"]);

        testData.PostRequest["PostRequest"].UserId = user.Id;

        var response = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Posts + Endpoints.AccessToken,
                testData.PostRequest["PostRequest"]);

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Posts}");
        
        await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
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
                            Body = "",
                            Title = TestServices.NewId,
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