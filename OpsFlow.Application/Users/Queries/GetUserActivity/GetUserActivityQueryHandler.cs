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

            // getUser
            AppUser user = await _userService.FindByIdAsync
            (
                request.userId
            ) ?? throw new NotFoundException("Wanted user not found!");

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

            // sorting
            query = query.OrderByDescending(x => x.OccuredAt);

            // getTotalCount
            int totalCount = query.Count();

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // pagination
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new UserActivityItemDto
                (
                    user.Id,
                    user.FullName,
                    user.UserName,
                    x.EventType,
                    x.OccuredAt,
                    x.IncidentId,
                    x.TaskId
                )).ToList();

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