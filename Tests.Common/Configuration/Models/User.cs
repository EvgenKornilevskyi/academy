using Newtonsoft.Json;
using Tests.Common.Configuration.Interfaces;

namespace Tests.Common.Configuration.Models
{
    public class User : IIdentity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("gender")]
        public string? Gender { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is User))
            {
                return false;
            }
            return (Id == ((User)obj).Id)
                   && (Name == ((User)obj).Name)
                   && (Email == ((User)obj).Email
                   && (Gender == ((User)obj).Gender)
                   && (Status == ((User)obj).Status));
        }
    }
}