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
    public class PutUsers : TestBase
    {
        [Test]
        [Category("Put")]
        [TestCaseSource(typeof(TestDataSourceUsers), nameof(TestDataSourceUsers.PutRequestUpdatesUserAllFields))]
        public async Task PutRequest_UpdateUserAllFields_ExpectedUserUpdated(TestData testData)
        {
            var initialUser = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["initialUserRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Put(Endpoints.Users + Endpoints.UserId(initialUser.Id)
                 + Endpoints.AccessToken, testData.UserRequest["updatedUserRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var updatedUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, initialUser);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(updatedUser.Name, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(updatedUser.Email, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(updatedUser.Gender, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(updatedUser.Status, Is.EqualTo(testData.UserRequest["updatedUserRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });
        }
    }
}
