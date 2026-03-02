using Microsoft.AspNetCore.Identity;

namespace OpsFlow.Application.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName {get; set;}
        public DateTime CreatedAt {get; set;}
        public AppRole Role {get; set;}
    }
}