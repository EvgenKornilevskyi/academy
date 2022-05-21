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
    public class DeleteUsers : TestBase
    {
        [Test]
        [Category("Delete")]
        [TestCaseSource(typeof(TestDataSourceUsers), nameof(TestDataSourceUsers.DeleteRequestReturnsStatusNoContent))]
        public async Task DeleteRequest_DeleteUser_ExpectedStatusCodeReturned(TestData testData)
        {
            var user = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            var responseDelete = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Users + Endpoints.UserId(user.Id)
                + Endpoints.AccessToken);

            Assert.That(responseDelete.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
        }
    }
}


