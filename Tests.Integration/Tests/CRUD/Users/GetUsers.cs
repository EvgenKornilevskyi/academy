using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.Users
{
    public class GetUsers : TestBase
    {
        [Test]
        [Category("Get")]
        [TestCaseSource(typeof(TestDataSourceGet), nameof(TestDataSourceGet.GetRequestReturnsUser))]
        public async Task GetRequest_GetUser_ExpectedUserReturned(TestData testData)
        {
            var responsePost = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                 testData.UserRequest["GetRequest"]);
            var responsePostContent = await responsePost.Content.ReadAsStringAsync();
            var responsePostUserId = JsonConvert.DeserializeObject<UserSingleResponse>(responsePostContent).User.Id;

            var responseGet = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(responsePostUserId) 
                 + Endpoints.AccessToken);
            var responseGetContent = await responseGet.Content.ReadAsStringAsync();
            var responseUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseGetContent).User;

            Assert.Multiple(() =>
            {
                Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["StatusCodeOK"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(responseUser.Id.ToString, Is.EqualTo(responsePostUserId.ToString()),
                    "Actual Id isnt equal to expected.");
                Assert.That(responseUser.Name, Is.EqualTo(testData.UserRequest["GetRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(responseUser.Email, Is.EqualTo(testData.UserRequest["GetRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(responseUser.Gender, Is.EqualTo(testData.UserRequest["GetRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(responseUser.Status, Is.EqualTo(testData.UserRequest["GetRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });

            await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Users + Endpoints.UserId(responsePostUserId)
                + Endpoints.AccessToken);
        }

        internal static class TestDataSourceGet
        {
            internal static IEnumerable GetRequestReturnsUser
            {
                get
                {
                    var data = new TestData();

                    data.UserRequest["GetRequest"] = new User();

                    data.UserRequest["GetRequest"].Id = TestServices.Rand;
                    data.UserRequest["GetRequest"].Name = TestServices.NewId.ToString();
                    data.UserRequest["GetRequest"].Email = TestServices.NewId.ToString() + "@mail.com";
                    data.UserRequest["GetRequest"].Gender = "male";
                    data.UserRequest["GetRequest"].Status = "active";

                    data.StatusCode["StatusCodeOK"] = HttpStatusCode.OK;

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("ReturnsUserById");
                }
            }
        }
    }
}
