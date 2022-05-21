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

                data.UserRequest["UserRequest"] = CreateExpectedUserResponse();

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
        internal static IEnumerable PutRequestUpdatesUserAllFields
        {
            get
            {
                var data = new TestData();

                data.UserRequest["initialUserRequest"] = CreateExpectedUserResponse();

                data.UserRequest["updatedUserRequest"] = CreateExpectedUserResponse();

                data.StatusCode["StatusCode"] = HttpStatusCode.OK;

                yield return new TestCaseData(data)
                    .SetArgDisplayNames("ValidRequestShouldReturn200");
            }
        }
        internal static IEnumerable PatchRequestUpdatesUserEmail
        {
            get
            {
                var data = new TestData();

                data.UserRequest["initialUserRequest"] = CreateExpectedUserResponse();

                data.UserRequest["updatedUserRequest"] = new User()
                {
                    Id = data.UserRequest["initialUserRequest"].Id,
                    Name = data.UserRequest["initialUserRequest"].Name,
                    Email = "updatedEmail" + "@mail.com",
                    Gender = data.UserRequest["initialUserRequest"].Gender,
                    Status = data.UserRequest["initialUserRequest"].Status
                };

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
