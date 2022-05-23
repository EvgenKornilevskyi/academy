﻿using System.Collections;
using System.Net;
using NUnit.Framework;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Helpers;
using Tests.Common.Configuration;
using Tests.Common.Configuration.Models;
using Tests.Common.Configuration.Services.Creators;
using Tests.Common.Configuration.TestData;

namespace Tests.Integration.Tests.CRUD.Negative.Posts.WithoutToken;

    public class Patch : TestBase
{
    [Test]
    [Category("PatchPostWithoutToken")]
    [TestCaseSource(typeof(TestDataSourcePost), nameof(TestDataSourcePost.PatchRequestReturnsStatusCodeNotFound))]
    public async Task DeleteRequestWithoutToken(TestData testData)
    {
        var user = await IdentityCreator.CreateIdentity(Endpoints.Users,
            testData.UserRequest["PatchRequest"]);

        testData.PostRequest["PatchRequest"].UserId = user.Id;

        var post = await IdentityCreator.CreateIdentity(Endpoints.Posts,
            testData.PostRequest["PatchRequest"]);

        testData.PostRequest["newPatchRequest"].UserId = user.Id;
        var response = await TestServices.HttpClientFactory
                 .SendHttpRequestTo(HttpApisNames.Jsonplaceholder).Patch(Endpoints.Posts + 
                                                                         Endpoints.PostId(post.Id), 
                     testData.PostRequest["newPatchRequest"]);
            

        Assert.That(response.StatusCode, Is.EqualTo(testData.StatusCode["StatusCode"]),
            $"Actual StatusCode isn't equal to expected. {Endpoints.Posts}");

        await IdentityCreator.DeleteIdentity(Endpoints.Users, user);
        await IdentityCreator.DeleteIdentity(Endpoints.Posts, post);
    }


    private static class TestDataSourcePost
    {
        internal static IEnumerable PatchRequestReturnsStatusCodeNotFound
        {
            get
            {
                var data = new TestData
                {
                    UserRequest =
                    {
                        ["PatchRequest"] = new User
                        {
                            Name = TestServices.NewId,
                            Email = TestServices.NewId + "@mail.com",
                            Gender = "male",
                            Status = "active"
                        }
                    },

                    PostRequest =
                    {
                        ["PatchRequest"] = new Common.Configuration.Models.Post
                        {
                            Body = TestServices.NewId,
                            Title = TestServices.NewId
                        },
                        
                        ["newPatchRequest"] = new Common.Configuration.Models.Post()
                        {
                            Title = "Random Post Changed",
                            Body = "Random Post Changed Body"
                        }
                    },

                    StatusCode =
                    {
                        ["StatusCode"] = HttpStatusCode.NotFound
                    }
                };

                yield return new TestCaseData(data).SetArgDisplayNames("NotFoundReturn404");
            }
        }
    }
}

