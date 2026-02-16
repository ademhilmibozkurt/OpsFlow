using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public class AssignTaskCommandHandler
    {
        private readonly IIncidentTaskRepository _taskRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public AssignTaskCommandHandler(
            IIncidentTaskRepository taskRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUserService,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _historyRepository = historyRepository;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AssignTaskCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // checkPermission
            _permissionService.CanAssignTask(user);

            // assignTask
            IncidentTask task = await _taskRepository.GetByIdAsync(command.taskId);
            task.Assign(command.userId);

            // addHistory
            IncidentHistory history = IncidentHistory.AddTaskHistory(task.IncidentId, user.Id, IncidentTaskState.Assigned, _timeProvider.Now(), task.Id);
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            return task.Id;
        }
    }
}