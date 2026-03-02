using Microsoft.AspNetCore.Identity;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
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

        public async Task<AppUser> CreateUserAsync(string fullName, string userName, string email, string phoneNumber, string password)
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
                throw new UserCreationException("User not created!");
            }

            // addRole - if creation secceded
            await _userManager.AddToRoleAsync(user, AppRole.User);

            // returnUser
            return await _userManager.FindByEmailAsync(
                user.Email ?? 
                throw new NotFoundException("User not found!"));
        }

        public async Task<AppUser?> FindByIdAsync(string userId)
            => await _userManager.FindByIdAsync(userId);
        
        public async Task<AppUser?> FindByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<IQueryable<AppUser?>> FindByUserNameAsync(string userName)
            => _userManager.Users.Where(x => x.UserName == userName);

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        public async Task UpdateAsync(AppUser user)
            => await _userManager.UpdateAsync(user);

        public async Task Delete(AppUser user)
            => await _userManager.DeleteAsync(user);
    }
}