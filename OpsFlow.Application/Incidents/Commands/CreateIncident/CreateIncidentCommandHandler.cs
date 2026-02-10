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
        private readonly IIncidentHistoryRepository _incidentHistoryRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;

        // dependency injection
        public CreateIncidentCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository incidentHistoryRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService)
        {
            _incidentRepository = incidentRepository;
            _incidentHistoryRepository = incidentHistoryRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<int> Handle(CreateIncidentCommand command)
        {
            // getCurrentUser
            var user = _currentUser.Get();

            // checkPermission
            if (!_permissionService.CanCreateIncident(user))
            {
                throw new ForbiddenException("User not authenticated for creating incident operation!");
            }

            // createIncident
            Incident incident =  new Incident(command.title, command.description, command.createdById);
            await _incidentRepository.AddAsync(incident);

            // addHistory
            IncidentHistory history = new IncidentHistory(incident.Id, user.Id, IncidentState.Open, DateTime.UtcNow);
            await _incidentHistoryRepository.AddAsync(history);

            return incident.Id;
        }
        
    }
}