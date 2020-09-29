using Microsoft.AspNetCore.Authorization;

namespace CookieDave.Web.Data
{
    public class ApplicationUser
    {
        public string Email { get; set; }
        //public string FullName { get; set; }
        //public bool IsAdmin { get; set; }
        public string CDRole { get; set; }
    }

    public static class CDRole
    {
        public const string Tier1 = "Tier1";
        public const string Tier2 = "Tier2";
        public const string Admin = "Admin";
    }


    // https://stackoverflow.com/a/24182340/26086
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) => Roles = string.Join(",", roles);
    }
}
