using System.Net;
using Tests.Common.Configuration.Models;

namespace Tests.Common.Configuration.TestData
{
    public class TestData
    {
        public Dictionary<string, string> Strings = new Dictionary<string, string>();
        public Dictionary<string, string> Pagination = new Dictionary<string, string>();
        public Dictionary<string, User> PostRequest = new Dictionary<string, User>();
        public Dictionary<string, HttpStatusCode> StatusCode = new Dictionary<string, HttpStatusCode>();
        public Dictionary<string, User> User = new Dictionary<string, User>();
    }
}
