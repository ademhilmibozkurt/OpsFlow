using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;

using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.CreateIncident
{
    public class CreateIncidentCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        private readonly IUnitOfWork _unitOfWork;

        // dependency injection
        public CreateIncidentCommandHandler(
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

        public async Task<int> Handle(CreateIncidentCommand command)
        {
            // getCurrentUser
            var user = _currentUserService.Get();

            // checkPermission
            _permissionService.CanCreateIncident(user);

            // createIncident
            Incident incident =  Incident.Create(command.title, command.description, user.Id);
            await _incidentRepository.AddAsync(incident);

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(incident.Id, user.Id, IncidentState.Open, DateTime.UtcNow);
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            return incident.Id;
        }
        
    }
}