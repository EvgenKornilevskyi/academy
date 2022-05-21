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

namespace Tests.Integration.Tests.CRUD.Users
{
    internal static class TestDataSourceUsers
    {
        internal static IEnumerable PostRequestReturnsStatusCodeCreated
        {
            get
            {
                var data = new TestData();

                data.UserRequest["PostRequest"] = CreateExpectedUserResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.Created;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn201");
            }
        }
        internal static IEnumerable GetRequestReturnsUser
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable PutRequestUpdatesUser
        {
            get
            {
                var data = new TestData();

                data.UserRequest["InitialUserRequest"] = CreateExpectedUserResponse();

                data.UserRequest["UpdatedUserRequest"] = CreateExpectedUserResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable PatchRequestUpdatesUser
        {
            get
            {
                var data = new TestData();

                data.UserRequest["InitialUserRequest"] = CreateExpectedUserResponse();

                data.UserRequest["UpdatedUserRequest"] = CreateExpectedUserResponse();
                data.UserRequest["UpdatedUserRequest"].Email = TestServices.NewId.ToString() + "@mail.com";

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable DeleteRequestReturnsStatusNoContent
        {
            get
            {
                var data = new TestData();

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.NoContent;

                yield return new TestCaseData(data).SetArgDisplayNames("ValidRequestShouldReturn204");
            }
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
