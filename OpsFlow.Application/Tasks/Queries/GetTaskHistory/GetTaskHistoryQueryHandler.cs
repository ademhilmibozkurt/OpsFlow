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

            // findIncident
            Incident incident = await _incidentRepository.GetByIdAsync
            (
                request.incidentId,
                cancellationToken
            ) ?? throw new NotFoundException("Incident not found!");

            // getTask
            IncidentTask task = incident.GetTask(request.taskId);

            // checkPermission
            _permissionService.CanGetTaskHistory(task.CreatedById, userId, userRole);

            // getQuery
            IQueryable<IncidentHistory> query = _historyRepository.Query(cancellationToken);

            // getHistory
            query = query.Where(x => x.TaskId == task.Id);

            // sorting
            query = query.OrderByDescending(x => x.OccuredAt);

             // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // getTotalCount
            int totalCount = query.Count();

            // paginate
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new HistoryItemDto
                (
                    task.Id,
                    incident.Id,
                    x.PerformedById,
                    task.Title,
                    x.Note,
                    x.EventType,
                    x.OccuredAt
                )).ToList();  

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