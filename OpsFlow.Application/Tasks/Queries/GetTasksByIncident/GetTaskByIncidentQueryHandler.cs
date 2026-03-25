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

            // setPageSize
            int pageSize = request.PageSize > 100 ? 100 : request.PageSize;
            int pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;

            // findIncident
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
            ) ?? throw new NotFoundException("Incident not found!");

            // sorting
            query = query.OrderByDescending(x => x.CreatedAt);

            // getTotalCount
            int totalCount = query.Count();

            // pagination
            var items = query
                .Skip((pageNumber -1) * pageSize)
                .Take(pageSize)
                .Select(x => new TaskListItemDto
                (
                    x.Id,
                    x.IncidentId,
                    x.CreatedById,
                    x.AssigneeId,
                    x.Title,
                    x.Note,
                    x.TaskState,
                    x.CreatedAt,
                    x.AbortionNote
                )).ToList();

            // returnDto
            return new PaginatedResponseDto<TaskListItemDto>
            (
                items,
                pageNumber,
                pageSize,
                totalCount
            );
        }
    }
}