using Newtonsoft.Json;
using System.Collections.Generic;
using Tests.Common.Configuration.Models.Responses;

namespace Tests.Common.Configuration.Models
{
    public class CommentsResponse : BaseResponse
    {
        //public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public List<Comment>? Comments { get; set; }
    }
}
