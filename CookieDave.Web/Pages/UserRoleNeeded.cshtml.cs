using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieDave.Web.Pages
{
    [Authorize]
    public class UserRoleNeededModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
