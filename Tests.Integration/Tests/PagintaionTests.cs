using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.Pagination
{
    [TestFixture]
    public class PagintaionTests : TestBase
    {
        [Test]
        [Category("Pagination")]
        [TestCaseSource(typeof(TestDataSource), nameof(TestDataSource.GetPage))]
        public async Task Get_EntitiesByPage_ReturnsExpectedPageNumber(TestData testData)
        {
            //Arrange
            var expectedPage = testData.Pagination["Pagination"].Page;
            var endpoint = testData.Strings["Endpoint"];
            var response = await TestServices.HttpClientFactory.SendHttpRequestTo(HttpApisNames.Jsonplaceholder)
                .Get(endpoint + $"?page={expectedPage}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<BaseResponse>(responseBody);

            //Act
            var actualPage = entities?.Meta?.Pagination.Page;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                    $"Actual StatusCode isn't equal to expected.");
                Assert.That(actualPage, Is.EqualTo(expectedPage),
                    $"Actual Page isn't equal to expected.");
            });
        }

        [Test]
        [Category("Pagination")]
        public async Task Get_LimitPosts_Returns20Posts()
        {
            //Arrange
            var response = await TestServices.HttpClientFactory.SendHttpRequestTo(HttpApisNames.Jsonplaceholder)
                .Get(Endpoints.Posts);
            var responseBody = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<PostsResponse>(responseBody);

            //Act
            var actualLimit = entities.Meta.Pagination.Limit;
            var expectedPostsCount = entities.Posts.Count;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                    $"Actual StatusCode isn't equal to expected.");
                Assert.That(actualLimit, Is.LessThanOrEqualTo(expectedPostsCount),
                    $"Actual Limit isn't equal to expected.");
            });
        }

        [Test]
        [Category("Pagination")]
        public async Task Get_LimitUsers_Returns20Users()
        {
            //Arrange
            var response = await TestServices.HttpClientFactory.SendHttpRequestTo(HttpApisNames.Jsonplaceholder)
                .Get(Endpoints.Users);
            var responseBody = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<UsersResponse>(responseBody);

            //Act
            var actualLimit = entities.Meta.Pagination.Limit;
            var expectedUsersCount = entities.Users.Count;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                    $"Actual StatusCode isn't equal to expected.");
                Assert.That(actualLimit, Is.LessThanOrEqualTo(expectedUsersCount),
                    $"Actual Limit isn't equal to expected.");
            });
        }

        [Test]
        [Category("Pagination")]
        public async Task Get_LimitComments_Returns20Comments()
        {
            //Arrange
            var response = await TestServices.HttpClientFactory.SendHttpRequestTo(HttpApisNames.Jsonplaceholder)
                .Get(Endpoints.Comments);
            var responseBody = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<CommentsResponse>(responseBody);

            //Act
            var actualLimit = entities.Meta.Pagination.Limit;
            var expectedCommentsCount = entities.Comments.Count;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                    $"Actual StatusCode isn't equal to expected.");
                Assert.That(actualLimit, Is.LessThanOrEqualTo(expectedCommentsCount),
                    $"Actual Limit isn't equal to expected.");
            });
        }

        internal static class TestDataSource
        {
            internal static IEnumerable GetPage
            {
                get
                {
                    var postsData = new TestData();

                    postsData.Pagination["Pagination"] = new Common.Configuration.Models.Pagination();
                    postsData.Pagination["Pagination"].Page = 1;
                    postsData.Strings["Endpoint"] = Endpoints.Posts;

                    var commentsData = new TestData();

                    commentsData.Pagination["Pagination"] = new Common.Configuration.Models.Pagination();
                    commentsData.Pagination["Pagination"].Page = 1;
                    commentsData.Strings["Endpoint"] = Endpoints.Comments;
                    
                    var usersData = new TestData();

                    usersData.Pagination["Pagination"] = new Common.Configuration.Models.Pagination();
                    usersData.Pagination["Pagination"].Page = 1;
                    usersData.Strings["Endpoint"] = Endpoints.Users;

                    yield return new TestCaseData(postsData)
                        .SetArgDisplayNames("GetPage");
                    
                    yield return new TestCaseData(commentsData)
                        .SetArgDisplayNames("GetPage");
                    
                    yield return new TestCaseData(usersData)
                        .SetArgDisplayNames("GetPage");
                }
            }  
        }
    }
}

