using Microsoft.AspNetCore.Identity;
using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<AppUser> CreateUserAsync
        (
            string fullName,
            string userName,
            string email, 
            string phoneNumber,
            string password
        );
        Task<AppUser?> FindByIdAsync(string userId);
        Task<AppUser?> FindByEmailAsync(string email);
        Task<IQueryable<AppUser?>> FindByUserNameAsync(string userName); 
        Task<string> GenerateChangeEmailTokenAsync(AppUser user, string newEmail);
        Task<IdentityResult> ChangeEmailAsync(AppUser user, string newEmail, string token);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task UpdateAsync(AppUser user);
        Task Delete(AppUser user);
        Task<IQueryable<AppUser>> Query();
    }
}