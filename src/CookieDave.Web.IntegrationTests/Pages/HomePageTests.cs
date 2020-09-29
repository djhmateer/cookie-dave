using System.Threading.Tasks;
using CookieDave.Web.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CookieDave.Web.IntegrationTests.Pages
{
    public class HomePageTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public HomePageTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ReturnsPageWithExpectedH1()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/");

            //response.EnsureSuccessStatusCode();
            response.AssertOk();

            using var content = await HtmlHelpers.GetDocumentAsync(response);

            var h1 = content.QuerySelector("h1");

            Assert.Equal("Welcome to CookieDave", h1.TextContent);
        }
    }
}
