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

namespace Tests.Integration.Tests.CRUD.Comments
{
    internal class TestDataSourceComments
    {
        internal static IEnumerable PatchRequestUpdatesCommentEmail
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

                data.CommentRequest["initialCommentRequest"] = CreateExpectedCommentResponse();

                data.CommentRequest["updatedCommentRequest"] = new Comment()
                {
                    Id = data.CommentRequest["initialCommentRequest"].Id,
                    PostId = data.CommentRequest["initialCommentRequest"].PostId,
                    Name = data.CommentRequest["initialCommentRequest"].Name,
                    Email = "Changedcomment@email.com",
                    Body = data.CommentRequest["initialCommentRequest"].Body
                };

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable DeleteRequestReturnsStatusCodeNoContent
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

                data.CommentRequest["CommentRequest"] = CreateExpectedCommentResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.NoContent;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn204");
            }
        }
        internal static IEnumerable PutRequestUpdatesCommentAllFields
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

                data.CommentRequest["initialCommentRequest"] = CreateExpectedCommentResponse();

                data.CommentRequest["updatedCommentRequest"] = new Comment()
                {
                    Id = TestServices.Rand,
                    PostId = TestServices.Rand,
                    Name = TestServices.NewId,
                    Email = TestServices.NewId + "@mail.com",
                    Body = "Random Comment Changed Body"
                };

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable GetRequestReturnsComment
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

                data.CommentRequest["CommentRequest"] = CreateExpectedCommentResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable PostRequestReturnsStatusCodeCreated
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

                data.CommentRequest["CommentRequest"] = CreateExpectedCommentResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.Created;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn201");
            }
        }

        private static Comment CreateExpectedCommentResponse()
        {
            var expectedComment = new Comment()
            {
                Id = TestServices.Rand,
                PostId = TestServices.Rand,
                Name  = TestServices.NewId,
                Email = TestServices.NewId + "@mail.com",
                Body = "Random comment"
            };
            return expectedComment;
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
    }
}
