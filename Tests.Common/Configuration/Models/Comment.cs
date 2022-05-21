using Newtonsoft.Json;
using Tests.Common.Configuration.Interfaces;

namespace Tests.Common.Configuration.Models
{
    public class Comment : IIdentity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("post_id")]
        public int PostId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("body")]
        public string? Body { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Comment))
            {
                return false;
            }
            return (PostId == ((Comment)obj).PostId)
                   && (Name == ((Comment)obj).Name)
                   && (Email == ((Comment)obj).Email)
                   && (Body == ((Comment)obj).Body);
        }
    }
}
