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

namespace Tests.Integration.Tests.CRUD.Posts
{
    internal static class TestDataSourcePosts
    {
        internal static IEnumerable PatchRequestUpdatesPost
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["initialPostRequest"] = CreateExpectedPostResponse();

                data.PostRequest["updatedPostRequest"] = new Post()
                {
                    Id = TestServices.Rand,
                    UserId = TestServices.Rand,
                    Title = "Random Post Changed",
                    Body = "Random Post Changed Body"
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

                data.StatusCode["StatusCode"] = HttpStatusCode.NoContent;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturnNoContent");
            }
        }
        internal static IEnumerable PutRequestUpdatesPost
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["initialPostRequest"] = CreateExpectedPostResponse();

                data.PostRequest["updatedPostRequest"] = new Post()
                {
                    Id = TestServices.Rand,
                    UserId = TestServices.Rand,
                    Title = "Random Post Changed",
                    Body = "Random Post Changed Body"
                };

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable GetRequestReturnsPost
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.PostRequest["PostRequest"] = CreateExpectedPostResponse();

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

                data.StatusCode["StatusCode"] = HttpStatusCode.Created;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn201");
            }
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
