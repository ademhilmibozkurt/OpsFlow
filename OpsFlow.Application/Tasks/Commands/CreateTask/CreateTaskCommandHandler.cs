using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUserService,
            IPermissionService permissionService,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTaskCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // checkPermission
            _permissionService.CanCreateTask();

            // createTask
            

            // addHistory

            // save

            return taskId;
        }
    }
}