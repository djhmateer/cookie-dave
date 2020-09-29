using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CookieDave.Web.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AuthenticationTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            _factory = factory;
        }

        [Theory]
        [InlineData("/Tier1RoleNeeded")]
        [InlineData("/Tier2RoleNeeded")]
        [InlineData("/AdminRoleNeeded")]
        [InlineData("/Crawl")]

        [InlineData("/Admin/Staff/Add")]
        [InlineData("/Admin/Courts/Bookings/Upcoming")]
        [InlineData("/Admin/Courts/Booking/1/Cancel")]
        [InlineData("/Admin/Courts/Maintenance/Upcoming")]
        [InlineData("/FindAvailableCourts")]
        [InlineData("/BookCourt/1")]
        [InlineData("/Bookings")]
        public async Task Get_SecurePageRedirectsAnUnauthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.StartsWith("http://localhost/identity/account/login", response.Headers.Location.OriginalString, StringComparison.OrdinalIgnoreCase);
        }
    }

}
