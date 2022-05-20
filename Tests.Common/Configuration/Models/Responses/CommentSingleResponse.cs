using Newtonsoft.Json;

namespace Tests.Common.Configuration.Models.Responses
{
    public class CommentSingleResponse
    {
        public Meta? Meta { get; set; }
        [JsonProperty("data")]
        public Comment Comment { get; set; }
    }
}
