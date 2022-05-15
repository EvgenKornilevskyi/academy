using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models.Responses;

namespace Tests.Integration.Tests
{
    public class PaginationTests : TestBase
    {
        [TestCase(Endpoints.Posts, 1)]
        [TestCase(Endpoints.Posts, 2)]
        [TestCase(Endpoints.Posts, 3)]
        [TestCase(Endpoints.Comments, 1)]
        [TestCase(Endpoints.Comments, 2)]
        [TestCase(Endpoints.Comments, 3)]
        [TestCase(Endpoints.Users, 1)]
        [TestCase(Endpoints.Users, 2)]
        [TestCase(Endpoints.Users, 3)]
        public async Task Get_EntitiesByPage_ReturnsExpectedPageNumber(string endpoint, int expectedPage)
        {
            //Arrange
            var response = await HttpClient.Get(endpoint + $"?page={expectedPage}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<BaseResponse>(responseBody);

            //Act
            var actualPage = entities?.Meta?.Pagination.Page;

            //Assert
            Assert.AreEqual(expectedPage, actualPage);
        }
    }
}
