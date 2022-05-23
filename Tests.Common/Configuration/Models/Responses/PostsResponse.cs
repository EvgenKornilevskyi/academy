using Newtonsoft.Json;
using Tests.Common.Configuration.Models.Responses;

namespace Tests.Common.Configuration.Models
{
    public class PostsResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Post>? Posts { get; set; }
    }
}
