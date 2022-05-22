using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Users.WithoutToken;

public class Post : TestBase
{
    [Test]
    [Category("PostUserWithoutToken")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnauthorized))]
    public async Task PostRequestWithoutToken(TestData testData)
    {
        var response = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users,
                testData.UserRequest["PostRequest"]);

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");
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
                    StatusCode =
                    {
                        ["StatusCode"] = HttpStatusCode.Unauthorized
                    }
                };

                yield return new TestCaseData(data).SetArgDisplayNames("UnauthorizedReturn401");
            }
        }
    }
}