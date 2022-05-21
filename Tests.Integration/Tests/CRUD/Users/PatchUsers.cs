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

namespace Tests.Integration.Tests.CRUD.Users
{
    public class PatchUsers : TestBase
    {
        [Test]
        [Ignore("Ignore a test")]
        [Category("Patch")]
        [TestCaseSource(typeof(TestDataSourceUsers), nameof(TestDataSourceUsers.PatchRequestUpdatesUser))]
        public async Task PutRequest_UpdateUser_ExpectedUserUpdated(TestData testData)
        {
            var user = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["InitialUserRequest"]);

            var responsePatch = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Patch(Endpoints.Users + Endpoints.UserId(user.Id)
                 + Endpoints.AccessToken, testData.UserRequest["UpdatedUserRequest"].Email);

            var responseGet = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(user.Id)
                 + Endpoints.AccessToken);
            var responseGetContent = await responseGet.Content.ReadAsStringAsync();
            var responseGetUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseGetContent).User;

            Assert.Multiple(() =>
            {
                Assert.That(responsePatch.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(responseGetUser.Email, Is.EqualTo(testData.UserRequest["UpdatedUserRequest"].Email),
                    "Actual Email isnt equal to expected.");
            });

            await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
        }
    }
}
