using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieDave.Web.Pages
{
    //[Authorize(Roles = "User")]
    public class UserRoleNeededModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
