using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenRepository _tokenRepository;
        public ChangePasswordCommandHandler(
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            ITokenRepository tokenRepository)
        {
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _tokenRepository = tokenRepository;
        }
        
        public async Task<ChangePasswordResponseDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");;

            // getUser
            AppUser user = await _userService.FindByIdAsync(
                userId)
                ?? throw new NotFoundException("User not found!");

            // checkPermission
            _permissionService.CanChangePassword(userId, user.Id);

            // checkPassword
            bool result = await _userService.CheckPasswordAsync(user, request.newPassword);

            // changePassword
            await _userService.ChangePasswordAsync(user, request.currentPassword, request.newPassword);
            DateTime changedAt = _timeProvider.Now();
        
            // revokeTokens
            await _tokenRepository.RevokeAllAsync(userId, cancellationToken);

            // emailNotification

            // checkUserName
            if(user.UserName == null) throw new NullReferenceException();

            // returnDto
            return new ChangePasswordResponseDto
            (
                user.UserName,
                changedAt
            );
        }
    }
}