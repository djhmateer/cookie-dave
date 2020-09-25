using System.Collections.Generic;
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

    //public enum CDRole
    //{
    //    Anonymous,
    //    Tier1,
    //    Tier2,
    //    Admin
    //

    static class CDRole
    {
        public const string Tier1 = "Tier1";
        public const string Tier2 = "Tier2";
        public const string Admin = "Admin";
        //public static string AtLeastTier1 { get; set; }
        //public const string AtLeastTier1 = AtLeastTier1x();

        //public const string AtLeastTier1 = Tier1 + "," + Tier2;

        //public const string AtLeastTier2 = Tier1 + "," + Tier2;

        //public static string AtLeastTier1x() => AddCommas(new List<string> { Tier1, Tier2, Admin });

        //private static string AddCommas(List<string> things)
        //{
        //    var foo = "";
        //    foreach (var thing in things)
        //    {
        //        foo += thing + ",";
        //    }

        //    var ridOfLast = foo.Remove(foo.Length - 1, 1);
        //    return ridOfLast;
        //jk}
    }


    // https://stackoverflow.com/a/24182340/26086
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }

    //public class CDPolicy
    //{
    //    public const string AtLeastTier1 = "AtLeastTier1";
    //    public const string AtLeastTier2 = "AtLeastTier2";
    //    public const string AdminOnly = "AdminOnly";
    //}
}
