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
    public class PutUsers : TestBase
    {
        [Test]
        [Category("Put")]
        [TestCaseSource(typeof(TestDataSourcePut), nameof(TestDataSourcePut.PutRequestUpdatesUser))]
        public async Task PutRequest_UpdateUser_ExpectedUserUpdated(TestData testData)
        {
            var initialUser = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["PostRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Put(Endpoints.Users + Endpoints.UserId(initialUser.Id)
                 + Endpoints.AccessToken, testData.UserRequest["PutRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, initialUser);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["PutRequest"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(updatedUser.Name, Is.EqualTo(testData.UserRequest["PutRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(updatedUser.Email, Is.EqualTo(testData.UserRequest["PutRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(updatedUser.Gender, Is.EqualTo(testData.UserRequest["PutRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(updatedUser.Status, Is.EqualTo(testData.UserRequest["PutRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });
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

                    data.StatusCode["PutRequest"] = HttpStatusCode.OK;

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("UpdatesUserById");
                }
            }
        }
    }
}
