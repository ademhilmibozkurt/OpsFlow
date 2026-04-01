using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedResponseDto<UserItemDto>>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;

        public GetUsersQueryHandler
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

        public async Task<PaginatedResponseDto<UserItemDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // checkPermission
            _permissionService.CanGetUsers(userRole);

            // getQuery
            IQueryable<AppUser> query = await _userService.Query();

            // filtering
            if (request.getConfirmed)
            {
                query = query.Where(x => x.EmailConfirmed);   
            }

            // sorting
            query = query.OrderByDescending(x => x.CreatedAt);

            // getTotalCount
            int totalCount = query.Count();

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // pagination
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new UserItemDto
                (
                    x.Id,
                    x.FullName,
                    x.UserName,
                    x.Email,
                    x.PhoneNumber,
                    x.Role,
                    x.CreatedAt
                )).ToList();

            // returnDto
            return new PaginatedResponseDto<UserItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}