using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Users.Queries.GetUserActivity
{
    public class GetUserActivityQueryHandler : IRequestHandler<GetUserActivityQuery, PaginatedResponseDto<UserActivityItemDto>>
    {
        private readonly IUserService _userService;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;

        public GetUserActivityQueryHandler
        (
            IUserService userService,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService
        )
        {
            _userService = userService;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<PaginatedResponseDto<UserActivityItemDto>> Handle(GetUserActivityQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // checkPermission
            _permissionService.CanGetUserActivity(userRole);

            // getUserQuery
            IQueryable<AppUser> userQuery = await _userService.Query();

            // getHistoryQuery
            IQueryable<IncidentHistory> query = _historyRepository.Query(cancellationToken);

            // filtering - PerformedBy == userId
            query = query.Where(x => x.PerformedById == request.userId);

            // filterByTask
            if (request.onlyTasks)
            {
                query = query.Where(x => x.TaskId != null);
            }

            // filterByDate
            if (request.fromDate.HasValue)
            {
                query = query.Where(x => x.OccuredAt >= request.fromDate);
            }

            if (request.toDate.HasValue)
            {
                query = query.Where(x => x.OccuredAt <= request.toDate);
            }

            // getTotalCount
            int totalCount = query.Count();

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // sorting + joining + pagination
            var items = query
                .OrderByDescending(h => h.OccuredAt)
                .Join
                (
                    userQuery,
                    h => h.PerformedById,
                    u => u.Id,
                    (h,u) => new UserActivityItemDto
                    (
                        u.Id,
                        u.FullName,
                        u.UserName,
                        h.EventType,
                        h.OccuredAt,
                        h.IncidentId,
                        h.TaskId
                    )
                )
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToList(); 

            // returnDto
            return new PaginatedResponseDto<UserActivityItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}