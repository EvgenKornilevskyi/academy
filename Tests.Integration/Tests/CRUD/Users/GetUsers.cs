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
    public class GetUsers : TestBase
    {
        [Test]
        [Category("Get")]
        [TestCaseSource(typeof(TestDataSource), nameof(TestDataSource.ReturnsUserById))]
        public async Task GetRequest_GetUser_ExpectedUserReturned(TestData testData)
        {
            var responsePost = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(Endpoints.Users + Endpoints.AccessToken,
                 testData.PostRequest["PostRequest"]);
            var responsePostContent = await responsePost.Content.ReadAsStringAsync();
            var responsePostUserId = JsonConvert.DeserializeObject<UserSingleResponse>(responsePostContent).User.Id.ToString();

            Assert.That(responsePost.StatusCode, Is.EqualTo(testData.StatusCode["StatusCodeCreated"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");

            var responseGet = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + "/" + responsePostUserId + Endpoints.AccessToken);
            var responseGetContent = await responseGet.Content.ReadAsStringAsync();
            var responseUser = JsonConvert.DeserializeObject<UserSingleResponse>(responseGetContent).User;

            Assert.Multiple(() =>
            {
                Assert.That(responseGet.StatusCode, Is.EqualTo(testData.StatusCode["StatusCodeOK"]),
                    $"Actual StatusCode isnt equal to expected. {Endpoints.Users}" + $"{testData.PostRequest["PostRequest"].Email}");
                Assert.That(responseUser.Id.ToString, Is.EqualTo(responsePostUserId),
                    "Actual Id isnt equal to expected.");
                Assert.That(responseUser.Name, Is.EqualTo(testData.PostRequest["PostRequest"].Name),
                    "Actual Name isnt equal to expected.");
                Assert.That(responseUser.Email, Is.EqualTo(testData.PostRequest["PostRequest"].Email),
                    "Actual Email isnt equal to expected.");
                Assert.That(responseUser.Gender, Is.EqualTo(testData.PostRequest["PostRequest"].Gender),
                    "Actual Gender isnt equal to expected.");
                Assert.That(responseUser.Status, Is.EqualTo(testData.PostRequest["PostRequest"].Status),
                    "Actual Status isnt equal to expected.");
            });
        }

        internal static class TestDataSource
        {
            internal static IEnumerable ReturnsUserById
            {
                get
                {
                    var data = new TestData();

                    data.PostRequest["PostRequest"] = new User();

                    data.PostRequest["PostRequest"].Id = TestServices.Rand;
                    data.PostRequest["PostRequest"].Name = "MyLife";
                    data.PostRequest["PostRequest"].Email = "beLike@mail.com";
                    data.PostRequest["PostRequest"].Gender = "male";
                    data.PostRequest["PostRequest"].Status = "active";

                    data.StatusCode["StatusCodeOK"] = HttpStatusCode.OK;
                    data.StatusCode["StatusCodeCreated"] = HttpStatusCode.Created;

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("ReturnsUserById");
                }
            }
        }
    }
}
