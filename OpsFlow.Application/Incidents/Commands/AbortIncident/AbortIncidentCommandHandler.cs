using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.AbortIncident
{
    public class AbortIncidentCommandHandler : IRequestHandler<AbortIncidentCommand, AbortIncidentResponseDto>
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

        public async Task<AbortIncidentResponseDto> Handle(AbortIncidentCommand request, CancellationToken cancellationToken)
        {
            // chechPermission - !! check current user is null or not !!
            _permissions.CanAbortIncident(_currentUser.Role);

            // abortIncident - !! check is null or not !!
            Incident incident = await _incidentRepository.GetByIdAsync(request.incidentId);
            incident.Abort(_currentUser.UserId);

            // createTimestamp
            DateTime abortedAt = _timeProvider.Now();

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(request.incidentId, _currentUser.UserId, IncidentState.Aborted, abortedAt, request.abortionNote);
            await _historyRepository.AddAsync(history);

            // save
            _unitOfWork.Commit();

            
            return new AbortIncidentResponseDto
            (
                incident.Id,
                request.abortionNote,
                _currentUser.UserId,
                abortedAt
            );
        }
    }
}