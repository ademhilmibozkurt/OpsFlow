using System.IO.Compression;
using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Tasks.Queries.GetTaskHistory
{
    public class GetTaskHistoryQueryHandler : IRequestHandler<GetTaskHistoryQuery, PaginatedResponseDto<HistoryItemDto>>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;


        public GetTaskHistoryQueryHandler
        (
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService
        )
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<PaginatedResponseDto<HistoryItemDto>> Handle(GetTaskHistoryQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // getJoinedQueries
            var query =
                from h in _historyRepository.Query(cancellationToken)
                join t in _incidentRepository.TaskQuery(cancellationToken)
                on h.TaskId equals t.Id
                where t.Id == request.taskId
                select new HistoryItemDto
                (
                    t.Id,
                    h.PerformedById,
                    t.Title,
                    h.Note,
                    h.EventType,
                    h.OccuredAt
                );

            // getTotalCount
            int totalCount = query.Count();

             // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // pagination + sorting
            var items = query
                .OrderByDescending(h => h.OccuredAt)
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToList() 
                ?? throw new NotFoundException("Task history not found!");  

            // returnDto
            return new PaginatedResponseDto<HistoryItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}