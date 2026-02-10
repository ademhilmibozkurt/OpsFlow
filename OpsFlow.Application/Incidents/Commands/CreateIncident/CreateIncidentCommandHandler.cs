using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;

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


        // getCurrentUser

        // checkPermission

        // createIncident

        // addHistory

        // save
    }
}