using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, ChangeRoleResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly ITokenRepository _tokenRepository;
        private readonly RoleManager<AppRole> _roleManager;
        public ChangeRoleCommandHandler(
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            ITokenRepository tokenRepository,
            RoleManager<AppRole> roleManager)
        {
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _tokenRepository = tokenRepository;
            _roleManager = roleManager;
        }
        
        public async Task<ChangeRoleResponseDto> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");

            // getUser
            AppUser user = await _userService.FindByIdAsync(
                request.userId)
                ?? throw new NotFoundException("User not found!");

            // checkRole
            bool exists = await _roleManager.RoleExistsAsync(request.userRole.ToString());
            if (exists == false) throw new NotFoundException("Role not found in AppRole!");

            // checkPermission
            _permissionService.CanChangeRole(user.Role.ToString());

            // changeRole
            user.Role = request.userRole;
            
            // updateUser
            await _userService.UpdateAsync(user);

            // revokeTokens
            await _tokenRepository.RevokeAllAsync(user.Id, cancellationToken);

            // nullCheckUserName
            if(user.UserName == null) throw new NullReferenceException("UserName is null!");

            // returnDto
            return new ChangeRoleResponseDto
            (
                user.FullName,
                user.UserName,
                user.Role
            );
        }
    }
}