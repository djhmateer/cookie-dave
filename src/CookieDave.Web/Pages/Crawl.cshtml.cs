using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CookieDave.Web.Data.CDRole;

namespace CookieDave.Web.Pages
{
    //[Authorize(Roles = "Tier1Role,Tier2Role")]
    [Authorize(Roles = Tier1AndTier2Role)]
    public class CrawlModel : PageModel
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public string Message { get; set; }

        //public string Message { get; set; }   
        public CrawlModel(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        //public void OnGet()
        public void OnGet()
        {
            // how to get the User and Roles/Policy???

            var name = httpContextAccessor.HttpContext.User.Identity.Name;

            //var claims = httpContextAccessor.HttpContext.User.Claims;
            //foreach (var claim in claims)
            //{
            //    var t = claim.Value;
            //}

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
