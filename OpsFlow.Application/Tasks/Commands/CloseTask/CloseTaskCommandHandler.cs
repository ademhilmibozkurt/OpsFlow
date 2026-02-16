using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.CloseTask
{
    public class CloseTaskCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public CloseTaskCommandHandler(
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
        
        public async Task<int> Handle(CloseTaskCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();
            
            // findTask
            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            IncidentTask task = incident.GetTask(command.taskId);

            // checkPermission
            _permissionService.CanCloseTask(user, task.AssignedId);

            // finishTask
            task.Finish(user.Id);

            // addHistory
            IncidentHistory history = IncidentHistory.AddTaskHistory(incident.Id, user.Id, IncidentTaskState.Done, _timeProvider.Now(), task.Id);
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            return task.Id;
        }
    }
}