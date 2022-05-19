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
    public class PatchUsers : TestBase
    {
        [Test]
        [Ignore("Ignore a test")]
        [Category("Patch")]
        [TestCaseSource(typeof(TestDataSourcePatch), nameof(TestDataSourcePatch.PutRequestUpdatesUser))]
        public async Task PutRequest_UpdateUser_ExpectedUserUpdated(TestData testData)
        {
            var user = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["PostRequest"]);

            var responsePatch = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Patch(Endpoints.Users + Endpoints.UserId(user.Id)
                 + Endpoints.AccessToken, testData.UserRequest["PatchRequest"].Email);

            var responseGet = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(user.Id)
                 + Endpoints.AccessToken);
            var responseGetContent = await responseGet.Content.ReadAsStringAsync();
            var responseGetUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseGetContent).User;

            Assert.Multiple(() =>
            {
                Assert.That(responsePatch.StatusCode, Is.EqualTo(testData.StatusCode["StatusCodeOK"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(testData.UserRequest["PatchRequest"].Email, Is.EqualTo(responseGetUser.Email),
                    "Actual Email isnt equal to expected.");
            });

            await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
        }

        internal static class TestDataSourcePatch
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

                    data.UserRequest["PatchRequest"] = new User();

                    //data.UserRequest["PutRequest"].Id = TestServices.Rand;
                    //data.UserRequest["PutRequest"].Name = TestServices.NewId.ToString();
                    data.UserRequest["PatchRequest"].Email = TestServices.NewId.ToString() + "@mail.com";
                    //data.UserRequest["PutRequest"].Gender = "male";
                    //data.UserRequest["PutRequest"].Status = "active";

                    data.StatusCode["StatusCodeOK"] = HttpStatusCode.OK;

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("UpdatesUserById");
                }
            }
        }
    }
}
