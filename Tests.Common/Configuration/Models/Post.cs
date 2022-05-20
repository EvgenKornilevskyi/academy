using Newtonsoft.Json;
using Tests.Common.Configuration.Interfaces;

namespace Tests.Common.Configuration.Models
{
    public class Post : IIdentity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("body")]
        public string? Body { get; set; }
    }
}
