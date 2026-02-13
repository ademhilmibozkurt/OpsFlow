using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.ChangePriority
{
    public class ChangePriorityCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPermissionService _permissionService;
        
        // dependency injection
        public ChangePriorityCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUserService,
            IPermissionService permissionService)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUserService = currentUserService;
            _permissionService = permissionService;
        }

        public async Task<int> Handle(ChangePriorityCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // checkPermission
            if (!_permissionService.CanChangePriority(user))
            {
                throw new ForbiddenException($"{user.Role} can not change incident priority!");
            }

            // changePriority
            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            incident.SetPriority(command.toPriority, user.Id);

            // addHistory
            IncidentHistory history = IncidentHistory.AddPriorityHistory(incident.Id, user.Id, command.toPriority, DateTime.UtcNow);
            await _historyRepository.AddAsync(history);

            // save UoW's job

            return incident.Id;
        }
    }
}