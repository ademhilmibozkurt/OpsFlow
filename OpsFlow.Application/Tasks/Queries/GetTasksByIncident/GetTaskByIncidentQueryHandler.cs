using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Tasks.Queries.GetTasksByIncident
{
    public class GetTasksByIncidentQueryHandler : IRequestHandler<GetTasksByIncidentQuery, PaginatedResponseDto<TaskListItemDto>>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;

        public GetTasksByIncidentQueryHandler
        (
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService
        )
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<PaginatedResponseDto<TaskListItemDto>> Handle(GetTasksByIncidentQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            /*// findIncident
            Incident incident = await _incidentRepository.GetByIdAsync
            (
                request.incidentId, 
                cancellationToken
            ) ?? throw new NotFoundException("Incident not found!");

            // checkPermission
            _permissionService.CanGetIncidentTasks(incident.CreatedById, userId, userRole);

            // getQuery
            IQueryable<IncidentTask> query = _incidentRepository.TaskQuery(cancellationToken); 
            
            // getTasks
            query = query.Where
            (
                x => x.IncidentId == incident.Id
            ) ?? throw new NotFoundException("Incident not found!");*/ 

            // getJoinedQueries 
            var query = 
                from i in _incidentRepository.Query(cancellationToken)
                join t in _incidentRepository.TaskQuery(cancellationToken)
                on i.Id equals t.IncidentId
                where t.IncidentId == request.incidentId
                select new TaskListItemDto
                (
                    t.Id,
                    t.IncidentId,
                    t.CreatedById,
                    t.AssigneeId,
                    t.Title,
                    t.Note,
                    t.TaskState,
                    t.CreatedAt,
                    t.AbortionNote
                );

            // checkPermission
            // _permissionService.CanGetIncidentTasks(incident.CreatedById, userId, userRole);

            // getTotalCount
            int totalCount = query.Count();

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // pagination + sorting
            var items = query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .ToList()
                ?? throw new NotFoundException("Tasks not found!");

            // returnDto
            return await Task.FromResult(new PaginatedResponseDto<TaskListItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            ));
        }
    }
}