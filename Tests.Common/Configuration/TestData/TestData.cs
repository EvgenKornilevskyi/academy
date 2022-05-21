using System.Net;
using Tests.Common.Configuration.Models;

namespace Tests.Common.Configuration.TestData
{
    public class TestData
    {
        public Dictionary<string, string> Strings = new Dictionary<string, string>();
        public Dictionary<string, Pagination> Pagination = new Dictionary<string, Pagination>();
        public Dictionary<string, User> UserRequest = new Dictionary<string, User>();
        public Dictionary<string, Post> PostRequest = new Dictionary<string, Post>();
        public Dictionary<string, List<Post>> PostRequestList = new Dictionary<string, List<Post>>();
        public Dictionary<string, HttpStatusCode> StatusCode = new Dictionary<string, HttpStatusCode>();
        public Dictionary<string, Comment> CommentRequest = new Dictionary<string, Comment>();
        public Dictionary<string, List<Comment>> CommentRequestList = new Dictionary<string, List<Comment>>();
    }
}
