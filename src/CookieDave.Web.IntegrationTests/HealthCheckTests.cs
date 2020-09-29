using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CookieDave.Web.IntegrationTests.Pages;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CookieDave.Web.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HealthCheckTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            var response = await _client.GetAsync("/healthcheck");

            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //response.EnsureSuccessStatusCode();
            response.AssertOk();
        }

        [Fact]
        public async Task HealthCheck_NoCache()
        {
            var response = await _client.GetAsync("/healthcheck");

            var header = response.Headers.CacheControl;

            // no caching yet
            Assert.Null(header);

            //Assert.False(header.MaxAge.HasValue);

            // cache on the client and any intermediaries?
            //Assert.False(header.Public);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
