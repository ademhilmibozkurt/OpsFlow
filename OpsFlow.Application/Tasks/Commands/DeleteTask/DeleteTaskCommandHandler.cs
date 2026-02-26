using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTaskCommandHandler(
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

        public async Task<DeleteTaskResponseDto> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new NotFoundException("User id not found!");
            string userRole = _currentUser.Role ?? throw new NotFoundException("User role not found!");

            // checkPermission
            _permissionService.CanDeleteTask(userRole);

            // findIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not incident!");

            // findTask
            IncidentTask task = incident.GetTask(request.taskId);

            // deleteTask
            task.Delete(userId);
            incident.DropTask(task.Id);
            DateTime deletedAt = _timeProvider.Now();

            // addHistory
            IncidentHistory history = IncidentHistory.AddTaskHistory(incident.Id, userId, IncidentTaskState.Deleted, deletedAt, task.Id);
            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync();
            
            return new DeleteTaskResponseDto
            (
                task.Id,
                userId
            );
        }
    }
}