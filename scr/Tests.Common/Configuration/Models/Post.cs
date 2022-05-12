using Newtonsoft.Json;

namespace Tests.Common.Configuration.Models
{
    public class Post
    {
        public int Id { get; set; }
        [JsonProperty("post_id")]
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
