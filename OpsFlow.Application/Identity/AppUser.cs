using Microsoft.AspNetCore.Identity;

namespace OpsFlow.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt {get; set;}
        public string Role {get; set;}
    }
}