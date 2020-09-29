using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CookieDave.Web.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CookieDave.Web.IntegrationTests.Pages
{
    public class Tier1NeededTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Tier1NeededTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            _factory = factory;
        }

        [Fact]
        public async Task Get_SecurePageIsForbiddenForAnUnauthenticatedUser()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Tier1RoleNeeded");

            // should be a 302Redirect
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/account/login", response.Headers.Location.OriginalString, StringComparison.OrdinalIgnoreCase);
        }

       
    }

}
