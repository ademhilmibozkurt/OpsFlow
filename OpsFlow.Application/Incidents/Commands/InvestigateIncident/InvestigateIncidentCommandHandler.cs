using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.InvestigateIncident
{
    public class InvestigateIncidentCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;
        public InvestigateIncidentCommandHandler(
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

        public async Task<int> Handle(InvestigateIncidentCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // checkPermission
            _permissionService.CanInvestigateIncident(user);

            // investigateIncident
            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            incident.Investigate(user.Id);

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(command.incidentId, user.Id, IncidentState.Investigating, DateTime.UtcNow);
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            return incident.Id;
        }
    }
}