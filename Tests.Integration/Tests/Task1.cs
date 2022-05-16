using NUnit.Framework;
using System.Collections;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests
{
    [TestFixture]
    public class Task1 : TestBase
    {
        [Test]
        [TestCaseSource(typeof(TestDataSource1), nameof(TestDataSource1.PostsRequestData))]
        public async Task HwTask1(TestData testData)
        {
            //var expectedPage = 6;
            //var response = await TestServices.HttpClientFactory
            //    .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Posts + $"?page={expectedPage}");
            //var responseBody = await response.Content.ReadAsStringAsync();
            //var posts = JsonConvert.DeserializeObject<PostsResponse>(responseBody);

            //var actualPage = posts?.Meta?.Pagination.Page;

            //Assert.AreEqual(expectedPage, actualPage);
        }
    }

    internal static class TestDataSource1
    {
        internal static IEnumerable PostsRequestData
        {
            get
            {
                var data = new TestData();
                yield return new TestCaseData(data).SetArgDisplayNames("PostsRequest");
            }
        }
    }
}
