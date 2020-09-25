using System.Security.Claims;
using CookieDave.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CookieDave.Web.Data.CDRole;

namespace CookieDave.Web.Pages
{
    [AuthorizeRoles(Tier1, Tier2, Admin)]
    public class CrawlModel : PageModel
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CrawlModel(IHttpContextAccessor httpContextAccessor) => this.httpContextAccessor = httpContextAccessor;

        public string? Message { get; set; }

        public void OnGet()
        {
            var name = httpContextAccessor.HttpContext.User.Identity.Name;

            Message = "roles: ";
            var claims = httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role);
            foreach (var claim in claims)
            {
                var role = claim.Value;
                Message += role + " ";
            }
        }
    }

}
