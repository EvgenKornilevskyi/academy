using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Users;

public class AlreadyExisting : TestBase
{
    [Test]
    [Category("PostAlreadyExistingUser")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnprocessableEntity))]
    
    public async Task GetNonExistentUser(TestData testData)
        {
            var user = await IdentityCreator.CreateIdentity(Endpoints.Users, 
                testData.UserRequest["PostRequest"]);
            
            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + 
                                                                       Endpoints.AccessToken,
                    testData.UserRequest["PostRequest"]);
            

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["UnprocessableEntity"]),
                $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");
            
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
                        StatusCode =
                        {
                            ["UnprocessableEntity"] = HttpStatusCode.UnprocessableEntity
                        }
                    };

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("UnprocessableEntityReturn422");
                }
            }
        }
}