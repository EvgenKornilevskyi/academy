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
    public class PostUsers : TestBase
    {
        [Test]
        [Category("Post")]
        [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PostRequestReturnsStatusCodeCreated))]
        public async Task PostRequest_PostUser_ExpectedStatusCodeReturned(TestData testData)
        {
            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                testData.UserRequest["PostRequest"]);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseUserId = JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User.Id;

            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");

            await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Users + Endpoints.UserId(responseUserId)
                + Endpoints.AccessToken);
        }
    }

    internal static class TestDataSourcePost
    {
        internal static IEnumerable PostRequestReturnsStatusCodeCreated
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

                data.StatusCode["StatusCode"] = HttpStatusCode.Created;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn201");
            }
        }
    }
}

