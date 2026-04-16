using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeUserName
{
    public class ChangeUserNameCommandHandler : IRequestHandler<ChangeUserNameCommand, ChangeUserNameResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenRepository _tokenRepository;
        public ChangeUserNameCommandHandler(
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

        public async Task<ChangeUserNameResponseDto> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");;

            // checkExistedUser
            if (await _userService.FindByUserNameAsync(request.newUserName) != null)
                throw new ForbiddenException("Provided user name exists!");

            // getUser
            AppUser user = await _userService.FindByIdAsync
            (
                userId
            )?? throw new NotFoundException("User not found!");

            // checkPermission
            _permissionService.CanChangeUserName(userId, user.Id);

            // changeUserName
            user.UserName = request.newUserName;
            await _userService.UpdateAsync(user);
            DateTime changedAt = _timeProvider.Now();

            // revokeTokens
            await _tokenRepository.RevokeAllAsync(userId, cancellationToken);

            // emailNotification

            // returnDto
            return new ChangeUserNameResponseDto
            (
                user.FullName,
                user.UserName,
                changedAt
            );
        }
    }
}