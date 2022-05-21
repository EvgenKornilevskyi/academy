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
        //[Ignore("Ignore a test")]
        [Category("Patch")]
        [TestCaseSource(typeof(TestDataSourceUsers), nameof(TestDataSourceUsers.PatchRequestUpdatesUserEmail))]
        public async Task PatchRequest_UpdatesUserEmail_ExpectedUserUpdated(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["initialUserRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Patch(Endpoints.Users + Endpoints.UserId(User.Id)
                 + Endpoints.AccessToken, testData.UserRequest["updatedUserRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(updatedUser.Email, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(updatedUser.Name, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(updatedUser.Gender, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(updatedUser.Status, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });
        }
    }
}
