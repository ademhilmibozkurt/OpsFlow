using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
    {
        private readonly IIncidentTaskRepository _taskRepository;
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskCommandHandler(
            IIncidentTaskRepository taskRepository,
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUserService,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTaskCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // checkPermission
            _permissionService.CanCreateTask(user);

            // checkIncident
            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            EnsureIncidentOpen(incident);

            // createTask
            IncidentTask task = IncidentTask.Create(command.incidentId, command.title, command.note);
            await _taskRepository.AddAsync(task);

            // addHistory - !!! TASK ID NASIL GELECEK DÜŞÜN !!!
            IncidentHistory history = IncidentHistory.AddTaskHistory(incident.Id, user.Id, IncidentTaskState.Created, _timeProvider.Now(), task.Id);
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            return task.Id;
        }

        private void EnsureIncidentOpen(Incident incident)
        {
            if (incident.State != IncidentState.Open)
            {
                throw new InvalidOperationException("Incident is not open. Can not add task!");
            }
        }
    }
}