using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieDave.Web.Pages
{
    [Authorize]
    public class AdminRoleNeededModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
