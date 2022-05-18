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
    public class DeleteUsers : TestBase
    {
        [Test]
        [Category("Delete")]
        [TestCaseSource(typeof(TestDataSourceDelete), nameof(TestDataSourceDelete.DeleteRequestReturnsStatusNoContent))]
        public async Task DeleteRequest_DeleteUser_ExpectedStatusCodeReturned(TestData testData)
        {
            var responsePost = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                testData.UserRequest["DeleteRequest"]);
            var responsePostContent = await responsePost.Content.ReadAsStringAsync();
            var responsePostUserId = JsonConvert.DeserializeObject<UserSingleResponse>(responsePostContent).User.Id;

            var responseDelete = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Users + Endpoints.UserId(responsePostUserId)
                + Endpoints.AccessToken);

            Assert.That(responseDelete.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
        }
    }

    internal static class TestDataSourceDelete
    {
        internal static IEnumerable DeleteRequestReturnsStatusNoContent
        {
            get
            {
                var data = new TestData();

                data.UserRequest["DeleteRequest"] = new User();

                data.UserRequest["DeleteRequest"].Id = TestServices.Rand;
                data.UserRequest["DeleteRequest"].Name = TestServices.NewId.ToString();
                data.UserRequest["DeleteRequest"].Email = TestServices.NewId.ToString() + "@mail.com";
                data.UserRequest["DeleteRequest"].Gender = "male";
                data.UserRequest["DeleteRequest"].Status = "active";

                data.StatusCode["StatusCode"] = HttpStatusCode.NoContent;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn204");
            }
        }
    }
}


