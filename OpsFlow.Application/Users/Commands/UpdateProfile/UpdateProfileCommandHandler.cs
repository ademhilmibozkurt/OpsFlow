using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        public UpdateProfileCommandHandler(
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider)
        {
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            
        }
        
        public async Task<UpdateProfileResponseDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");

            // findUser
            AppUser user = await _userService.FindByIdAsync(
                userId)
                ?? throw new NotFoundException("User not found!");

            // checkPermission
            _permissionService.CanUpdateProfile(userId, user.Id, user.Role.ToString());

            // updateUser
            user.FullName = request.fullName;
            user.PhoneNumber = request.phoneNumber;
            await _userService.UpdateAsync(user);
            DateTime updatedAt = _timeProvider.Now();

            // returnDto
            return new UpdateProfileResponseDto
            (
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                updatedAt
            );
        }
    }
}