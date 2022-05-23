using Newtonsoft.Json;

namespace Tests.Common.Configuration.Models.Responses
{
    public class PostSingleResponse
    {
        public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public Post Post { get; set; }
    }
}
