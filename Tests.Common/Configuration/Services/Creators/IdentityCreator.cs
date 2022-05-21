using Newtonsoft.Json;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.Common.Configuration.Interfaces;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
using Tests.Common.Configuration.TestData;

namespace Tests.Common.Configuration.Services.Creators
{
    public class IdentityCreator
    {
        public static async Task<IIdentity> CreateIdentity(string endpoint, object payload)
        {
            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(endpoint + Endpoints.AccessToken,
                 payload);
            if(response.StatusCode != HttpStatusCode.Created)
            {
                TestContext.WriteLine("Failed creating Identity");
                TestContext.WriteLine(response.StatusCode.ToString());
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            if (endpoint == Endpoints.Users)
            {
                return JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;
            }
            else if (endpoint == Endpoints.Posts)
            {
                return JsonConvert.DeserializeObject<PostSingleResponse>(responseContent).Post;
            }
            else
            {
                return JsonConvert.DeserializeObject<CommentSingleResponse>(responseContent).Comment;
            }
        }
        public static async Task DeleteIdentity(string endpoint, IIdentity identity)
        {
            await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(endpoint + Endpoints.UserId(identity.Id)
                + Endpoints.AccessToken);
        }
    }
}
