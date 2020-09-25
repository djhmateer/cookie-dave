using CookieDave.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CookieDave.Web.Data.CDRole;

namespace CookieDave.Web.Pages
{
    [AuthorizeRoles(Admin)]
    public class AdminRoleNeededModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
