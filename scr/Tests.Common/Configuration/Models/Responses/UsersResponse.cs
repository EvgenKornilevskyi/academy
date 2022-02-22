using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tests.Common.Configuration.Models
{
    public class UsersResponse
    {
        public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public List<User>? Users { get; set; }
    }
}
