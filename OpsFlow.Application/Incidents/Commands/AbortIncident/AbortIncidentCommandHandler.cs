using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.AbortIncident
{
    public class AbortIncidentCommandHandler
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissions;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public AbortIncidentCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissions,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissions = permissions;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AbortIncidentCommand command)
        {
            // getCurrentUser
            User user = _currentUserService.Get();

            // chechPermission
            _permissions.CanAbortIncident(user);

            // abortIncident
            Incident incident = await _incidentRepository.GetByIdAsync(command.incidentId);
            incident.Abort(user.Id);

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(command.incidentId, user.Id, IncidentState.Aborted, _timeProvider.Now());
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            return incident.Id;
        }
    }
}