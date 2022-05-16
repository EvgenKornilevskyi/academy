using NUnit.Framework;
using System.Collections;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests
{
    public class Task3 : TestBase
    {
        [TestCaseSource(typeof(TestDataSource3), nameof(TestDataSource3.GetRequest1))]
        public async Task HwTask3(TestData testData)
        {
            //var responce = await TestServices.HttpClientFactory
            //     .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Todos);
            //var responceContent = await responce.Content.ReadAsStringAsync();
            //var todosResponce = JsonConvert.DeserializeObject<List<TodosResponce>>(responceContent);

            //Assert place
        }
    }

    internal static class TestDataSource3
    {
        internal static IEnumerable GetRequest1
        {
            get
            {
                var data = new TestData();
                

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("PostsRequest1");
            }
        }
    }
}
