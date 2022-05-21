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
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Post))
            {
                return false;
            }
            return (UserId == ((Post)obj).UserId)
                   && (Title == ((Post)obj).Title)
                   && (Body == ((Post)obj).Body);
        }
    }
}
