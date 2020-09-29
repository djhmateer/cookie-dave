using System.Net;
using System.Threading.Tasks;
using CookieDave.Web.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CookieDave.Web.IntegrationTests
{
    public class ErrorTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ErrorTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.AllowAutoRedirect = false;
            _factory = factory;
        }

        [Theory]
        [InlineData("/asdfasdf")]
        [InlineData("/pagenothere")]
        public async Task Get_PageNotThere_ShouldReturn404AndCustomErrorPage(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var h1 = content.QuerySelector("h1");

            Assert.Equal("Sorry - 404 Not Found", h1.TextContent);
        }
    }

}
