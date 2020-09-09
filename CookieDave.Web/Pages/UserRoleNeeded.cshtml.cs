using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieDave.Web.Pages
{
    [Authorize(Roles = "Administrator")]
    public class UserRoleNeededModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
