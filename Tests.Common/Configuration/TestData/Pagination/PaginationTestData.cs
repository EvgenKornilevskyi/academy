using System.Collections;

namespace Tests.Common.Configuration.TestData.Pagination
{
    public class PaginationTestData
    {
        public static object[] GetAllPagination()
        {
            return _testData;
        }

        public static object[] GetPostsPagination()
        {
            return _testData.Take(3).ToArray();
        }
        public static object[] GetCommentsPagination()
        {
            return _testData.Skip(3).Take(3).ToArray();
        }
        public static object[] GetUsersPagination()
        {
            return _testData.Skip(6).Take(3).ToArray();
        }

        private static object[] _testData =
        {
            new object[] { Endpoints.Posts, 1},
            new object[] { Endpoints.Posts, 2},
            new object[] { Endpoints.Posts, 3},
            new object[] { Endpoints.Comments, 1},
            new object[] { Endpoints.Comments, 2},
            new object[] { Endpoints.Comments, 3},
            new object[] { Endpoints.Users, 1},
            new object[] { Endpoints.Users, 2},
            new object[] { Endpoints.Users, 3}
        };
    }
}
