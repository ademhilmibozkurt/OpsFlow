using Microsoft.AspNetCore.Identity;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Identity;

namespace OpsFlow.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        // dependencyInjection
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool Success, IEnumerable<string> Errors)> CreateUserAsync(string fullName, string userName, string email, string phoneNumber, string password)
        {
            // createAppUserInstance
            AppUser user = new AppUser
            {
                FullName = fullName,
                UserName = userName,
                Email    = email,
                PhoneNumber = phoneNumber
            };

            // createUser - with identity user manager
            IdentityResult result = await _userManager.CreateAsync(user, password);

            // returnResult - if not succeeded
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description));
            }

            // addRole - if creation secceded
            await _userManager.AddToRoleAsync(user, AppRole.User);

            // returnResult
            return (true, Enumerable.Empty<string>());
        }
        
        public async Task<AppUser?> FindByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IList<string>> GetRoleAsync(AppUser user)
            => await _userManager.GetRolesAsync(user);
    }
}