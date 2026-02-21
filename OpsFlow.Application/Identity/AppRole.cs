using Microsoft.AspNetCore.Identity;

namespace OpsFlow.Infrastructure.Identity
{
    public class AppRole : IdentityRole
    {
        public const string User = "User";
        public const string Admin = "Admin";
        public const string IncidentManager = "IncidentManager";
    }
}