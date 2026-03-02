using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly ITokenRepository _tokenRepository;
        public DeleteUserCommandHandler(
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            ITokenRepository tokenRepository)
        {
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _tokenRepository = tokenRepository;
        }
        
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // findUser
            AppUser user = await _userService.FindByIdAsync(
                request.userId)
                ?? throw new NotFoundException("User not found!");

            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("Current user not found!");

            // getUserRole
            string userRole = await _userService.GetRoleAsync(user);

            // checkPermission
            _permissionService.CanDeleteUser(request.userId, userId, userRole);
            
            // deleteUser
            await _userService.Delete(user);
            
            // revokeToken
            await _tokenRepository.RevokeAllAsync(user.Id, cancellationToken);            
        }
    }
}