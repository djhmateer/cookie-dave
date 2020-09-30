﻿using CookieDave.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static CookieDave.Web.Data.CDRole;

namespace CookieDave.Web.Pages
{
    [AuthorizeRoles(Tier2, Admin)]
    public class Tier2RoleNeeded : PageModel
    {
        public void OnGet()
        {
        }
    }
}
