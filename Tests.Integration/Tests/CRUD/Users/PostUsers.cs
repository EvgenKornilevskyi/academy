using Newtonsoft.Json;
using NUnit.Framework;
using Polly;
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
    public class PostUsers : TestBase
    {
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSourceUsers), nameof(TestDataSourceUsers.PostRequestReturnsStatusCodeCreated))]
        public async Task PostRequest_PostUser_ExpectedStatusCodeReturned(TestData testData)
        {
            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                testData.UserRequest["UserRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
        }
    }
}

