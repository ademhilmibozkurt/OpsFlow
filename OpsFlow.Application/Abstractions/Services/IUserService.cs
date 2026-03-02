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
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task UpdateAsync(AppUser user);
        Task Delete(AppUser user);
    }
}