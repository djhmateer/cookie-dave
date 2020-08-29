using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CookieDave.Web.Pages
{
    public class ThrowExceptionModel : PageModel
    {
        public void OnGet()
        {
            throw new Exception("user generated exception from code");
        }
    }
}
