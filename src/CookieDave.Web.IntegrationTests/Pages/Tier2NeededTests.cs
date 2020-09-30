using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CookieDave.Web.Data;
using CookieDave.Web.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CookieDave.Web.IntegrationTests.Pages
{
    public class Tier2NeededTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Tier2NeededTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            _factory = factory;
        }

        public static IEnumerable<object[]> RoleAccess => new List<object[]>
        {
            // 403Forbidden
            new object[] { CDRole.Tier1, HttpStatusCode.Forbidden },
            new object[] { CDRole.Tier2, HttpStatusCode.OK },
            new object[] { CDRole.Admin, HttpStatusCode.OK },
            new object[] { "anewrole", HttpStatusCode.Forbidden }
        };

        [Theory]
        [MemberData(nameof(RoleAccess))]
        public async Task Get_SecurePageAccessibleOnlyByAdminUsers(string role, HttpStatusCode expected)
        {
            var client = _factory
                .WithWebHostBuilder(x => x.WithAuthorisedUserInRole(role))
                .CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            var response = await client.GetAsync("/Tier2RoleNeeded");

            Assert.Equal(expected, response.StatusCode);
        }
    }
}
