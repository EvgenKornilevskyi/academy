using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System.Collections;
using System.Net;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;
using Tests.Integration.Tests.CRUD.Posts;

namespace Tests.Integration.Tests.CRUD.Posts
{
    public class DeletePosts : TestBase
    {
        [Test]
        [Category("Delete")]
        [TestCaseSource(typeof(TestDataSourcePosts), nameof(TestDataSourcePosts.DeleteRequestReturnsStatusCodeNoContent))]
        public async Task DeleteRequest_DeletePost_ExpectedStatusCodeReturned(TestData testData)
        {
            var User = await IdentityCreator.CreateIdentity(Endpoints.Users, testData.UserRequest["UserRequest"]);

            testData.PostRequest["PostRequest"].UserId = User.Id;

            var Post = await IdentityCreator.CreateIdentity(Endpoints.Posts, testData.PostRequest["PostRequest"]);

            var response = await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(Endpoints.Posts + Endpoints.PostId(Post.Id) + Endpoints.AccessToken);

            await IdentityCreator.DeleteIdentity(Endpoints.Users, User);
            
            Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
                $"Actual StatusCode isnt equal to expected. {Endpoints.Users}");
        }
    }
}
