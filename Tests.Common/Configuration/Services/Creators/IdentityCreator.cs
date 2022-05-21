using Newtonsoft.Json;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common.Configuration.Interfaces;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Models.Responses;
using Tests.Common.Configuration.TestData;

namespace Tests.Common.Configuration.Services.Creators
{
    public static class IdentityCreator
    {
        public static async Task<IIdentity> CreateIdentity(string endpoint, object payload)
        {
            var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(endpoint + Endpoints.AccessToken,
                 payload);
            var responsePostContent = await response.Content.ReadAsStringAsync();
            return endpoint switch
            {
                Endpoints.Users => JsonConvert.DeserializeObject<UserSingleResponse>(responsePostContent).User,
                Endpoints.Posts => JsonConvert.DeserializeObject<PostSingleResponse>(responsePostContent).Post,
                _ => JsonConvert.DeserializeObject<CommentSingleResponse>(responsePostContent).Comment
            };
        }
        public static async Task DeleteIdentity(string endpoint, IIdentity identity)
        {
            await TestServices.HttpClientFactory
                .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Delete(endpoint + Endpoints.UserId(identity.Id)
                + Endpoints.AccessToken);
        }
    }
}
