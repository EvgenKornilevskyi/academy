using Newtonsoft.Json;

namespace Tests.Common.Configuration.Models
{
    public class UserSingleResponse
    {
        public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public User User { get; set; }
    }
}