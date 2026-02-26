using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.InvestigateIncident
{
    public class InvestigateIncidentCommandHandler : IRequestHandler<InvestigateIncidentCommand, InvestigateIncidentResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        public InvestigateIncidentCommandHandler(
            IIncidentRepository incidentRepository,
            IIncidentHistoryRepository historyRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _incidentRepository = incidentRepository;
            _historyRepository = historyRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
            _timeProvider = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task<InvestigateIncidentResponseDto> Handle(InvestigateIncidentCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new NotFoundException("User id not found!");
            string userRole = _currentUser.Role ?? throw new NotFoundException("User role not found!");

            // checkPermission
            _permissionService.CanInvestigateIncident(userRole);

            // investigateIncident
            DateTime investigatedAt = _timeProvider.Now();
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId, 
                cancellationToken) 
                ?? throw new NotFoundException("Incident not found!");

            incident.Investigate(userId);

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(
                request.incidentId,
                userId,
                IncidentState.Investigating,
                investigatedAt
            );

            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync();

            return new InvestigateIncidentResponseDto
            (
                incident.Id,
                userId,
                investigatedAt
            );
        }
    }
}