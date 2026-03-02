using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, ChangeEmailResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenRepository _tokenRepository;
        public ChangeEmailCommandHandler(
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

        public async Task<ChangeEmailResponseDto> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");;

            // checkUserName
            AppUser existedUser = await _userService.FindByEmailAsync(request.newEmail);

            if (existedUser != null)
                throw new ForbiddenException("Provided email exists!");

            // getUser
            AppUser user = await _userService.FindByIdAsync(
                userId)
                ?? throw new NotFoundException("User not found!");

            // checkPermission
            _permissionService.CanChangeUserName(userId, user.Id);

            // changeUserName
            user.Email = request.newEmail;
            await _userService.UpdateAsync(user);
            DateTime changedAt = _timeProvider.Now();

            // revokeTokens
            await _tokenRepository.RevokeAllAsync(userId, cancellationToken);

            // emailNotification

            // returnDto
            return new ChangeEmailResponseDto
            (
                user.FullName,
                user.Email,
                changedAt
            );
        }
    }
}