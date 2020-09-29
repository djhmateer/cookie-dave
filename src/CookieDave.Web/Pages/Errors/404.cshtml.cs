using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookieDave.Web.Pages.Errors
{
    public class _404Model : PageModel
    {
        public void OnGet()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            //_telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            //{
            //    ["originalPath"] = originalPath
            //});
        }
    }
}
