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
    public class PutUsers : TestBase
    {
        [Test]
        [Category("Put")]
        [TestCaseSource(typeof(TestDataSourcePut), nameof(TestDataSourcePut.PutRequestUpdatesUser))]
        public async Task PutRequest_UpdateUser_ExpectedUserUpdated(TestData testData)
        {
            var responsePost = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                 testData.UserRequest["PostRequest"]);
            var responsePostContent = await responsePost.Content.ReadAsStringAsync();
            var responsePostUserId = JsonConvert.DeserializeObject<UserSingleResponse>(responsePostContent).User.Id;

            var responsePut = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Put(Endpoints.Users + Endpoints.UserId(responsePostUserId)
                 + Endpoints.AccessToken, testData.UserRequest["PutRequest"]);
            var responsePutContent = await responsePut.Content.ReadAsStringAsync();
            var responsePutUser = JsonConvert.DeserializeObject<UserSingleResponse>(responsePutContent).User;

            Assert.Multiple(() =>
            {
                Assert.That(responsePut.StatusCode, Is.EqualTo(testData.StatusCode["StatusCodeOK"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(responsePutUser.Name, Is.EqualTo(testData.UserRequest["PutRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(responsePutUser.Email, Is.EqualTo(testData.UserRequest["PutRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(responsePutUser.Gender, Is.EqualTo(testData.UserRequest["PutRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(responsePutUser.Status, Is.EqualTo(testData.UserRequest["PutRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });

            await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Users + Endpoints.UserId(responsePostUserId)
                + Endpoints.AccessToken);
        }

        internal static class TestDataSourcePut
        {
            internal static IEnumerable PutRequestUpdatesUser
            {
                get
                {
                    var data = new TestData();

                    data.UserRequest["PostRequest"] = new User();

                    data.UserRequest["PostRequest"].Id = TestServices.Rand;
                    data.UserRequest["PostRequest"].Name = TestServices.NewId.ToString();
                    data.UserRequest["PostRequest"].Email = TestServices.NewId.ToString() + "@mail.com";
                    data.UserRequest["PostRequest"].Gender = "male";
                    data.UserRequest["PostRequest"].Status = "active";

                    data.UserRequest["PutRequest"] = new User();

                    data.UserRequest["PutRequest"].Id = TestServices.Rand;
                    data.UserRequest["PutRequest"].Name = TestServices.NewId.ToString();
                    data.UserRequest["PutRequest"].Email = TestServices.NewId.ToString() + "@mail.com";
                    data.UserRequest["PutRequest"].Gender = "male";
                    data.UserRequest["PutRequest"].Status = "active";

                    data.StatusCode["StatusCodeOK"] = HttpStatusCode.OK;

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("UpdatesUserById");
                }
            }
        }
    }
}
