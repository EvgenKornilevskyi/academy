using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
using Tests.Common.Configuration.TestData;
using Tests.Common.Configuration.TestData.Pagination;

namespace Tests.Integration.Tests
{
    [TestFixture]
    public class PagintaionTests : TestBase
    {
        [Test]
        [Category("Pagination")]
        [TestCaseSource(typeof(PaginationTestData), nameof(PaginationTestData.GetAllPagination))]
        public async Task Get_EntitiesByPage_ReturnsExpectedPageNumber(string endpoint, int page)
        {
            //Arrange
            //var response = await TestServices.HttpClientFactory.SendHttpRequestTo(HttpApisNames.Jsonplaceholder)
            //    .Get(endpoint + $"?page={expectedPage}");
            //var responseBody = await response.Content.ReadAsStringAsync();
            //var entities = JsonConvert.DeserializeObject<BaseResponse>(responseBody);

            ////Act
            //var actualPage = entities?.Meta?.Pagination.Page;

            ////Assert
            //Assert.AreEqual(expectedPage, actualPage);
            // dotnet test --filter TestCategory=Pagination
        }

        [TestCaseSource(typeof(PaginationTestData), nameof(PaginationTestData.GetPostsPagination))]
        public async Task Get_LimitPosts_Returns20Posts(string endpoint, int page)
        {
            ////Arrange
            //var response = await HttpClient.Get(endpoint + $"?page={page}");
            //var responseBody = await response.Content.ReadAsStringAsync();
            //var entities = JsonConvert.DeserializeObject<PostsResponse>(responseBody);

            ////Act
            //var actualLimit = entities.Meta.Pagination.Limit;
            //var expectedUsersCount = entities.Posts.Count;

            ////Assert
            //Assert.AreEqual(expectedUsersCount, actualLimit);
        }
        
        [TestCaseSource(typeof(PaginationTestData), nameof(PaginationTestData.GetUsersPagination))]
        public async Task Get_LimitUsers_Returns20Users(string endpoint, int expectedPage)
        {
            ////Arrange
            //var response = await HttpClient.Get(endpoint + $"?page={expectedPage}");
            //var responseBody = await response.Content.ReadAsStringAsync();
            //var entities = JsonConvert.DeserializeObject<UsersResponse>(responseBody);

            ////Act
            //var actualLimit = entities.Meta.Pagination.Limit;
            //var expectedUsersCount = entities.Users.Count;

            ////Assert
            //Assert.AreEqual(expectedUsersCount, actualLimit);
        }

        [TestCaseSource(typeof(PaginationTestData), nameof(PaginationTestData.GetCommentsPagination))]
        public async Task Get_LimitComments_Returns20Comments(string endpoint, int expectedPage)
        {
            ////Arrange
            //var response = await HttpClient.Get(endpoint + $"?page={expectedPage}");
            //var responseBody = await response.Content.ReadAsStringAsync();
            //var entities = JsonConvert.DeserializeObject<CommentsResponse>(responseBody);

            ////Act
            //var actualLimit = entities.Meta.Pagination.Limit;
            //var expectedUsersCount = entities.Comments.Count;

            ////Assert
            //Assert.AreEqual(expectedUsersCount, actualLimit);
        }

        //internal static class TestDataSource
        //{
        //    internal static IEnumerable ReturnsUserById
        //    {
        //        get
        //        {
        //            var data = new TestData();

        //            data.Pagination["Pagination"] = ;

                    

        //            yield return new TestCaseData(data)
        //                .SetArgDisplayNames("ReturnsUserById");
        //        }
        //    }
        //}
    }
}
