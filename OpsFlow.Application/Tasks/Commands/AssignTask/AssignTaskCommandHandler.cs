using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public class AssignTaskCommandHandler : IRequestHandler<AssignTaskCommand, AssignTaskResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AssignTaskCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssignTaskResponseDto> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new NotFoundException("User id not found!");
            string userRole = _currentUser.Role ?? throw new NotFoundException("User role not found!");

            // checkPermission
            _permissionService.CanAssignTask(userRole);

            // assignTask
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");
            
            IncidentTask task = incident.GetTask(request.taskId);
            task.Assign(request.assigneeId, userId);
            DateTime assignedAt = _timeProvider.Now();

            // addHistory
            IncidentHistory history = IncidentHistory.AddTaskHistory(task.IncidentId, userId, IncidentTaskState.Assigned, assignedAt, task.Id);
            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync(cancellationToken);

            return new AssignTaskResponseDto
            (
                task.Id,
                task.Title,
                userId,
                request.assigneeId,
                assignedAt
            );
        }
    }
}