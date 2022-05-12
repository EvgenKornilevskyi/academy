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
    public class Task2 : TestBase
    {
        [TestCaseSource(typeof(TestDataSource2), nameof(TestDataSource2.GetRequestData))]
        public async Task HwTask2(TestData testData)
        {

            //var responce = await TestServices.HttpClientFactory
            //     .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Comments);
            //var responceContent = await responce.Content.ReadAsStringAsync();
            //var responceComments = JsonConvert.DeserializeObject<CommentsResponce>(responceContent);

            //Assert place
        }


        internal static class TestDataSource2
        {
            internal static IEnumerable GetRequestData
            {
                get
                {
                    var data = new TestData();

                    data.CommentsResponce["CommentsResponce"] = new CommentsResponce();

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("PostsRequest");
                }
            }
        }
    }
}
