using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Tests.Common.Configuration.Models.Responses;

namespace Tests.Common.Configuration.Models
{
    public class UsersResponse : BaseResponse
    {
        //public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public List<User>? Users { get; set; }
    }
}
