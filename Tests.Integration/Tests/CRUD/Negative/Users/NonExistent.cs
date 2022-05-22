using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Users;

public class NonExistent
{
    [Test]
    [Category("GetNonExistentUser")]
    [TestCaseSource(typeof(TestDataSourceGet), nameof(TestDataSourceGet.GetRequestReturnsStatusCodeNotFound))]
    
    public async Task GetNonExistentUser(TestData testData)
        {
            var responsePostUserId =  testData.UserRequest["GetRequest"].Id;

            var responseGet = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + 
                                                                       Endpoints.UserId( responsePostUserId) + 
                                                                       Endpoints.AccessToken);

            Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["NotFound"]),
                $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");

            await TestServices.HttpClientFactory.
                SendHttpRequestTo(HttpApisNames.Jsonplaceholder).
                Delete(Endpoints.Users + 
                       Endpoints.UserId(responsePostUserId) + Endpoints.AccessToken);
        }

    private static class TestDataSourceGet
        {
            internal static IEnumerable GetRequestReturnsStatusCodeNotFound
            {
                get
                {
                    var data = new TestData
                    {
                        UserRequest =
                        {
                            ["GetRequest"] = new User
                            {
                                Id = Convert.ToInt32(-1)
                            }
                        },
                        StatusCode =
                        {
                            ["NotFound"] = HttpStatusCode.NotFound
                        }
                    };

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("NotFoundReturn404");
                }
            }
        }
}