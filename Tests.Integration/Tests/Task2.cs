using NUnit.Framework;
using System.Collections;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests
{
    [TestFixture]
    public class Task2 : TestBase
    {
        [Test]
        [TestCaseSource(typeof(TestDataSource2), nameof(TestDataSource2.GetRequestData))]
        public async Task HwTask2(TestData testData)
        {
            //var responce = await TestServices.HttpClientFactory
            //     .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Get(Endpoints.Users + Endpoints.UserId(3307) +
            //     Endpoints.AccessToken + TestServices.AuthorizationToken);
            //var responceContent = await responce.Content.ReadAsStringAsync();
            //var responceUserResponse = JsonConvert.DeserializeObject<UserSingleResponse>(responceContent);
            //var user = responceUserResponse.User;

            //Assert place
        }


        internal static class TestDataSource2
        {
            internal static IEnumerable GetRequestData
            {
                get
                {
                    var data = new TestData();

                    //data.User["User"] = new User();

                    //data.User["User"].Id = 1;
                    //data.User["User"].Name = "Eugen";
                    //data.User["User"].Email = "EugenSuper@mail.com";
                    //data.User["User"].Gender = "male";
                    //data.User["User"].Status = "active";

                    yield return new TestCaseData(data)
                        .SetArgDisplayNames("User");
                }
            }
        }
    }
}
