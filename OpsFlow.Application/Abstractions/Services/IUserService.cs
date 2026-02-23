using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<(bool Success, IEnumerable<string> Errors)> CreateUserAsync
        (
            string fullName,
            string userName,
            string email, 
            string phoneNumber,
            string password
        );
        Task<AppUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IList<string>> GetRoleAsync(AppUser user);
    }
}