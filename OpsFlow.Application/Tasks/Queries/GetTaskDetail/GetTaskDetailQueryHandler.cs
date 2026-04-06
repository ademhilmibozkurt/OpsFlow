using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Tasks.Queries.GetTaskDetail
{
    public class GetTaskDetailQueryHandler : IRequestHandler<GetTaskDetailQuery, TaskDetailResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        public GetTaskDetailQueryHandler(
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService)
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<TaskDetailResponseDto> Handle(GetTaskDetailQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // getJoinedQueries 
            var query = 
                from i in _incidentRepository.Query(cancellationToken)
                join t in _incidentRepository.TaskQuery(cancellationToken)
                on i.Id equals t.IncidentId
                where t.Id == request.taskId
                select new TaskDetailResponseDto
                (
                    t.Id,
                    i.Id,
                    t.Title,
                    t.Note,
                    t.AbortionNote,
                    i.Title,
                    t.CreatedById,
                    t.AssigneeId,
                    t.CreatedAt
                );
                
            // checkPermission
            // _permissionService.CanGetTaskDetail(task.CreatedById, userId, userRole);

            // sorting
            query = query.OrderByDescending(t => t.CreatedAt);

            // returnDto
            return query.FirstOrDefault() ?? throw new NotFoundException("Task detail not found!");
        }
    }
}