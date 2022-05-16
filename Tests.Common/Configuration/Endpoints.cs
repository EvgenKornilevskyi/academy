namespace Tests.Common.Configuration
{
    public static class Endpoints
    {
        private const string _basePath = "public/v1/";
        public const string Users = _basePath + "/users";
        public const string AccessToken = "?access-token=";
        public const string Posts = _basePath + "/posts";
        public const string Comments = _basePath + "/comments";
        public const string Todos = _basePath + "/todos";
        public static string UserId(int userId) => $"/{userId}";
    }
}
