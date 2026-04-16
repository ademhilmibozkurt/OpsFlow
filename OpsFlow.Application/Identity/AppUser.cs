using Microsoft.AspNetCore.Identity;

namespace OpsFlow.Application.Identity
{
    public class AppUser : IdentityUser
    {
        public required string FullName {get; set;}
        public new required string UserName {get; set;}
        public new required string Email {get; set;}
        public new required string PhoneNumber {get; set;}
        public DateTime CreatedAt {get; set;}
        public AppRole Role {get; set;}
    }
}