using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Incidents.Commands.ChangePriority
{
    public class ChangePriorityCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        
        // dependency injection
        public ChangePriorityCommandHandler(
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

        public async Task<int> Handle(ChangePriorityCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // checkPermission
            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            _permissionService.CanChangePriority(incident.CreatedById, user);

            // changePriority
            incident.SetPriority(command.toPriority, user.Id);

            // addHistory
            IncidentHistory history = IncidentHistory.AddPriorityHistory(incident.Id, user.Id, command.toPriority, _timeProvider.Now());
            await _historyRepository.AddAsync(history);

            // save UoW's job
            _unitOfWork.Commit();

            return incident.Id;
        }
    }
}