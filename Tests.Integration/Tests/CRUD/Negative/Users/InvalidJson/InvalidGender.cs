using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Users.InvalidJson;

public class InvalidGender
{
    [Test]
    [Category("PostUserInvalidGender")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeUnprocessableEntity))]
    public async Task PostUserInvalidGender(TestData testData)
    {
        var response = await TestServices.HttpClientFactory
            .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                testData.UserRequest["PostRequest"]);

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Users}");
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
                            Gender = "",
                            Status = "active"
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