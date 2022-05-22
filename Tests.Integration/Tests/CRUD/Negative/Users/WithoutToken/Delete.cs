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

public class Delete : TestBase
{
    [Test]
    [Category("DeleteUserWithoutToken")]
    [TestCaseSource(typeof(TestDataSourceDelete), nameof(TestDataSourceDelete.DeleteRequestReturnsStatusNotFound))]
    public async Task DeleteRequestWithoutToken(TestData testData)
    {
        var initialUser = await IdentityCreator.CreateIdentity(Endpoints.Users, 
            testData.UserRequest["DeleteRequest"]);

        var responseDelete = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Users + 
                                                                     Endpoints.UserId(initialUser.Id));

        Assert.That(responseDelete.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");
            
        await IdentityCreator.DeleteIdentity(Endpoints.Users, initialUser);
    }

    private static class TestDataSourceDelete
    {
        internal static IEnumerable DeleteRequestReturnsStatusNotFound
        {
            get
            {
                var data = new TestData
                {
                    UserRequest =
                    {
                        ["DeleteRequest"] = new User
                        {
                            Name = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Gender = "male",
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