using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Tasks.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTaskCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUserService,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public Task<DeleteTaskResponseDto> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            User user = _currentUserService.Get();

            _permissionService.CanDeleteTask(user);

            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            IncidentTask task = incident.GetTask(command.taskId);
            task.Delete(user.Id);
            incident.DropTask(task.Id);

            IncidentHistory history = IncidentHistory.AddTaskHistory(incident.Id, user.Id, IncidentTaskState.Deleted, _timeProvider.Now(), task.Id);
            await _historyRepository.AddAsync(history);

            _unitOfWork.Commit();

            return task.Id;
        }
    }
}