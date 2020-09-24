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
    //    Tier1Role,
    //    Tier2Role,
    //    AdminRole
    //}

    public class CDRole
    {
        public const string Tier1Role = "Tier1Role";
        public const string Tier2Role = "Tier2Role";
        public const string AdminRole = "AdminRole";
    }

    public class CDPolicy
    {
        public const string Tier1Policy = "Tier1Policy";
        public const string Tier2Policy = "Tier2Policy";
        public const string AdminPolicy = "AdminPolicy";
    }
}
