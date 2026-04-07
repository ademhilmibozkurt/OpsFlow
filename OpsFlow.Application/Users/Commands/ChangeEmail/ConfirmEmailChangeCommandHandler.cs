using System.Security.Authentication;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public class ConfirmChangeEmailCommandHandler : IRequestHandler<ConfirmEmailChangeCommand>
    {
        private readonly IMediator _mediatr;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly ITokenRepository _tokenRepository;
        public ConfirmChangeEmailCommandHandler(
            IMediator mediatr,
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            ITokenRepository tokenRepository)
        {
            _mediatr = mediatr;
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _tokenRepository = tokenRepository;
        }

        public async Task Handle(ConfirmEmailChangeCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");

            // getUser
            AppUser user = await _userService.FindByIdAsync(
                userId)
                ?? throw new NotFoundException("User not found!");

            // checkPermission
            _permissionService.CanChangeUserName(userId, request.userId);

            // decodeToken
            byte[] decodedBytes = WebEncoders.Base64UrlDecode(request.token);
            string decodedToken = Encoding.UTF8.GetString(decodedBytes);

            // changeEmail
            var result = await _userService.ChangeEmailAsync(user, request.newEmail, decodedToken);
            
            if(!result.Succeeded)
                throw new InvalidRefreshTokenException("Email token invalid or expired!");

            // revokeTokens
            await _tokenRepository.RevokeAllAsync(user.Id, cancellationToken);
        }
    }
}