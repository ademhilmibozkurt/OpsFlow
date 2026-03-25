using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Tasks.Queries.GetTaskDetail
{
    public class GetTaskDetailCommandHandler : IRequestHandler<GetTaskDetailCommand, TaskDetailResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        public GetTaskDetailCommandHandler(
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService)
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<TaskDetailResponseDto> Handle(GetTaskDetailCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // findIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            // findTask
            IncidentTask task = incident.GetTask(
                request.taskId)
                ?? throw new NotFoundException("Task not found!");

            // checkPermission
            _permissionService.CanGetTaskDetail(task.CreatedById, userId, userRole);

            // returnDto
            return new TaskDetailResponseDto
            (
                task.Id,
                incident.Id,
                task.Title,
                task.Note,
                task.AbortionNote,
                incident.Title,
                task.CreatedById,
                task.AssigneeId,
                task.CreatedAt
            );
        }
    }
}