using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tests.Common.Configuration.Models
{
    public class CommentsResponse
    {
        public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public List<Comment>? Comments { get; set; }
    }
}
