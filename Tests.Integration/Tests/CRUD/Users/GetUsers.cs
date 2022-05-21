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
    public class GetUsers : TestBase
    {
        [Test]
        [Category("Get")]
        [TestCaseSource(typeof(TestDataSourceUsers), nameof(TestDataSourceUsers.GetRequestReturnsUser))]
        public async Task GetRequest_GetUser_ExpectedUserReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(User.Id) 
                 + Endpoints.AccessToken);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
                Assert.That(responseUser.Id, Is.EqualTo(User.Id),
                    "Actual Id isnt equal to expected.");
                Assert.That(responseUser.Name, Is.EqualTo(testData.UserRequest["UserRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(responseUser.Email, Is.EqualTo(testData.UserRequest["UserRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(responseUser.Gender, Is.EqualTo(testData.UserRequest["UserRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(responseUser.Status, Is.EqualTo(testData.UserRequest["UserRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });
        }
    }
}
