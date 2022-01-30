using System.Net;
using Tests.Common.Configuration.Domain;

namespace Tests.Common.Configuration.TestData
{
    public class TestData
    {
        public Dictionary<string, string> Strings = new Dictionary<string, string>();
        public Dictionary<string, PostsRequest> PostsRequest = new Dictionary<string, PostsRequest>();
        public Dictionary<string, CommentsResponce> CommentsResponce = new Dictionary<string, CommentsResponce>();
        public Dictionary<string, List<TodosResponce>> TodosResponce = new Dictionary<string, List<TodosResponce>>();
        public Dictionary<string, HttpStatusCode> StatusCode = new Dictionary<string, HttpStatusCode>();
    }
}