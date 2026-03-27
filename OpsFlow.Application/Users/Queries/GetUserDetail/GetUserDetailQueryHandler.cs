using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, GetUserDetailResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;

        public GetUserDetailQueryHandler
        (
            IUserService userService,
            ICurrentUserService currentUser,
            IPermissionService permissionService
        )
        {
            _userService = userService;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<GetUserDetailResponseDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // checkPermission
            _permissionService.CanGetUserDetail(userRole);

            // getQuery
            IQueryable<AppUser> query = await _userService.Query();

            // getUserDetail
            query = query.Where(x => x.Id == request.userId);
            AppUser user = query.FirstOrDefault() ?? throw new NotFoundException("User not found!");

            // returnDto
            return new GetUserDetailResponseDto
            (
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.Role,
                user.CreatedAt
            );
        }
    }
}