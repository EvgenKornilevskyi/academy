using Newtonsoft.Json;
using Polly;
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
using Polly.Retry;

namespace Tests.Common.Configuration.Services.Creators
{
    public class IdentityCreator
    {
        private const int _maxRetries = 3;
        private static AsyncRetryPolicy? retryPolicy;
        public static async Task<IIdentity> CreateIdentity(string endpoint, object payload)
        {
            retryPolicy = Policy.Handle<HttpRequestException>(ex => ex.StatusCode != HttpStatusCode.Created)
                                .WaitAndRetryAsync(_maxRetries,
                                retryAttempt =>
                                {
                                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                                }
                                );

            var response = await retryPolicy.ExecuteAsync(async () =>
            {
                var responseTry =  await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Post(endpoint + Endpoints.AccessToken,
                 payload);
                responseTry.EnsureSuccessStatusCode();
                return responseTry;
            });

            if(response.StatusCode != HttpStatusCode.Created)
            {
                TestContext.WriteLine("Failed to create Identity");
                TestContext.WriteLine($"Status Code : {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            switch (endpoint) 
            {
                case Endpoints.Users:
                    return JsonConvert.DeserializeObject<UserSingleResponse>(responseContent).User;
                case Endpoints.Posts:
                    return JsonConvert.DeserializeObject<PostSingleResponse>(responseContent).Post;
                default:
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
