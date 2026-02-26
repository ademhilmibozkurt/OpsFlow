using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.CloseIncident
{
    public class CloseIncidentCommandHandler : IRequestHandler<CloseIncidentCommand, CloseIncidentResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        
        public CloseIncidentCommandHandler(
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

        public async Task<CloseIncidentResponseDto> Handle(CloseIncidentCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new NotFoundException("User id not found!");
            string userRole = _currentUser.Role ?? throw new NotFoundException("User role not found!");

            // checkPermission
            _permissionService.CanCloseIncident(userRole);

            // closeIncident
            DateTime closedAt = _timeProvider.Now();
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");
                
            incident.Close(userId);

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(
                incident.Id,
                userId,
                IncidentState.Closed,
                closedAt);

            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync();

            return new CloseIncidentResponseDto
            (
                incident.Id,
                userId,
                closedAt
            );
        }
    }
}