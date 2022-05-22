using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.DifficultScenarios
{
    internal class DataSourceDifficultScenarious
    {
        internal static IEnumerable Returns21Posts
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                var _posts = new List<Post>();

                for(int i = 0; i < 21; i++)
                {
                    _posts.Add(CreateExpectedPostResponse());
                }

                data.PostRequestList["PostRequestList"] = _posts;

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("Returns21Posts");
            }
        }
        internal static IEnumerable ReturnsThreePosts
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequestList["PostRequestList"] = CreateExpectedThreePostResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ReturnsThreePosts");
            }
        }
        internal static IEnumerable ReturnsThreeComments
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();
                
                data.CommentRequestList["CommentRequestList"] = CreateExpectedThreeCommentResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ReturnsThreePosts");
            }
        }
        internal static IEnumerable ReturnsUpdatedComment
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

                data.CommentRequest["initialCommentRequest"] = new Comment()
                {
                    Id = 1,
                    PostId = 1,
                    Name = data.UserRequest["UserRequest"].Name,
                    Email = data.UserRequest["UserRequest"].Email,
                    Body = "intitial random comment"
                };

                data.CommentRequest["updatedCommentRequest"] = new Comment()
                {
                    Id = 1,
                    PostId = 1,
                    Name = data.UserRequest["UserRequest"].Name,
                    Email = data.UserRequest["UserRequest"].Email,
                    Body = "updated random comment"
                };

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ReturnsThreePosts");
            }
        }
        private static List<Comment> CreateExpectedThreeCommentResponse()
        {
            var expectedComments = new List<Comment>();
            expectedComments.Add(new Comment { Id = 1, PostId = 1, Name = "Come", Email = "Come@mail.com", Body = "and" });
            expectedComments.Add(new Comment { Id = 2, PostId = 2, Name = "Take", Email = "Take@mail.com", Body = "It" });
            expectedComments.Add(new Comment { Id = 3, PostId = 3, Name = "Leonid", Email = "Leonid@mail.com", Body = "First" });
            return expectedComments;
        }
        private static List<Post> CreateExpectedThreePostResponse()
        {
            var expectedPosts = new List<Post>();
            expectedPosts.Add(new Post { Id = 1, UserId = 1, Title = "Come", Body = "and" });
            expectedPosts.Add(new Post { Id = 2, UserId = 2, Title = "get", Body = "them" });
            expectedPosts.Add(new Post { Id = 3, UserId = 3, Title = "AAA", Body = "AAA" });
            return expectedPosts;
        }
        private static User CreateExpectedUserResponse()
        {
            var expectedUser = new User()
            {
                Id = TestServices.Rand,
                Name = TestServices.NewId,
                Email = TestServices.NewId + "@mail.com",
                Gender = "male",
                Status = "active"
            };
            return expectedUser;
        }
        private static Post CreateExpectedPostResponse()
        {
            var expectedPost = new Post()
            {
                Id = TestServices.Rand,
                UserId = TestServices.Rand,
                Title = "Random Post",
                Body = "Random Post Body"
            };
            return expectedPost;
        }
        private static Comment CreateExpectedCommentResponse()
        {
            var expectedComment = new Comment()
            {
                Id = TestServices.Rand,
                PostId = TestServices.Rand,
                Name = TestServices.NewId,
                Email = TestServices.NewId + "@mail.com",
                Body = "Random Comment Body"
            };
            return expectedComment;
        }
    }
}
