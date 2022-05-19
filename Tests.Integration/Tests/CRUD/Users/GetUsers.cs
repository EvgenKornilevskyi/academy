using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
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
            var user = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["GetRequest"]);

            var responseGet = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(user.Id) 
                 + Endpoints.AccessToken);
            var responseGetContent = await responseGet.Content.ReadAsStringAsync();
            var responseUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseGetContent).User;

            Assert.Multiple(() =>
            {
                Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["StatusCodeOK"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(responseUser.Id.ToString, Is.EqualTo(user.Id.ToString()),
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

            await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
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
