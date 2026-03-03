using System.Security.Authentication;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public class RequestChangeEmailCommandHandler : IRequestHandler<RequestChangeEmailCommand>
    {
        private readonly IMediator _mediatr;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        public RequestChangeEmailCommandHandler(
            IMediator mediatr,
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService)
        {
            _mediatr = mediatr;
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task Handle(RequestChangeEmailCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");

            // checkNewEmail
            AppUser? existedUser = await _userService.FindByEmailAsync(request.newEmail);
            if (existedUser != null)
                throw new ForbiddenException("Provided email exists!");

            // getUser
            AppUser user = await _userService.FindByIdAsync(
                userId)
                ?? throw new NotFoundException("User not found!");

            // checkPermission
            _permissionService.CanChangeUserName(userId, user.Id);

            // generateIdentityToken
            string changeEmailToken = await _userService.GenerateChangeEmailTokenAsync(user, request.newEmail);

            // encodeToken - will use frontend link
            string encodedToken = WebEncoders.Base64UrlEncode(
                Encoding.UTF8.GetBytes(changeEmailToken));

            // createConfirmLink
            string confirmLink =
                $"https://localhost:8000" +
                $"?userId={user.Id}" +
                $"&newEmail={request.newEmail}" +
                $"&token={encodedToken}";

            // sendEmailNotification
            await _mediatr.Publish();
        }
    }
}