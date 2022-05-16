using Newtonsoft.Json;
using System.Text;

namespace ResultsManager.Tests.Common.Helpers
{
    public static class HttpHelper
    {
        public static async Task<HttpResponseMessage> Post(this HttpClient client, string endpoint, object payload)
            => await client.PostAsync(endpoint, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

        public static async Task<HttpResponseMessage> Get(this HttpClient client, string endpoint)
            => await client.GetAsync(endpoint);

        public static async Task<HttpResponseMessage> Put(this HttpClient client, string endpoint, object payload)
            => await client.PutAsync(endpoint, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

        public static async Task<HttpResponseMessage> Patch(this HttpClient client, string endpoint, object payload)
            => await client.PatchAsync(endpoint, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));

        public static async Task<HttpResponseMessage> Delete(this HttpClient client, string endpoint)
            => await client.DeleteAsync(endpoint);

    }
}
