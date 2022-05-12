using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tests.Common.Configuration.Models
{
    public class PostsResponse
    {
        public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public List<Post>? Posts { get; set; }
    }
}
