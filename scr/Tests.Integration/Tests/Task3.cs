using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Domain;
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
                data.TodosResponce["TodosResponce"] = CreateExpectedTodosResponse();

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("PostsRequest1");
            }
        }

      
       private static List<TodosResponce> CreateExpectedTodosResponse()
       {
            var expectedTodos = new List<TodosResponce>();
            expectedTodos.Add(new TodosResponce { UserId = 1, Id = 1, Title = "delectus aut autem", Completed = false });
            expectedTodos.Add(new TodosResponce { UserId = 1, Id = 2, Title = "quis ut nam facilis et officia qui", Completed = false });
            expectedTodos.Add(new TodosResponce { UserId = 1, Id = 3, Title = "fugiat veniam minus", Completed = false });
            return expectedTodos;
       }
    }
}
