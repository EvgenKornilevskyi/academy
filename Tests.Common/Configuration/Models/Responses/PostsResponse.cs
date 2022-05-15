using Newtonsoft.Json;
using System.Collections.Generic;
using Tests.Common.Configuration.Models.Responses;

namespace Tests.Common.Configuration.Models
{
    public class PostsResponse : BaseResponse
    {
        //public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public List<Post>? Posts { get; set; }
    }
}
