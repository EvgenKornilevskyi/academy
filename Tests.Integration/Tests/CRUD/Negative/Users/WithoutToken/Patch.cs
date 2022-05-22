using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Users.WithoutToken;

public class Patch : TestBase
{
    [Test]
    [Category("PatchUserWithoutToken")]
    [TestCaseSource(typeof(TestDataSourcePatch), nameof(TestDataSourcePatch.PatchRequestReturnsStatusNotFound))]
    public async Task DeleteRequestWithoutToken(TestData testData)
    {
        var user = await IdentityCreator.CreateIdentity(Endpoints.Users, 
            testData.UserRequest["PatchRequest"]);

        var response = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Patch(Endpoints.Users + 
                                                                     Endpoints.UserId(user.Id),
                testData.UserRequest["newPatchRequest"]);

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");
            
        await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
    }

    private static class TestDataSourcePatch
    {
        internal static IEnumerable PatchRequestReturnsStatusNotFound
        {
            get
            {
                var data = new TestData
                {
                    UserRequest =
                    {
                        ["PatchRequest"] = new User
                        {
                            Id = TestServices.Rand,
                            Name = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Gender = "male",
                            Status = "active"
                        },
                        
                        ["newPatchRequest"] = new User()
                        {
                            Id = TestServices.Rand,
                            Name = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Gender = "female",
                            Status = "active"
                        }
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