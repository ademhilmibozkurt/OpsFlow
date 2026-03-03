using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Incidents.Commands.ChangePriority
{
    public class ChangePriorityCommandHandler : IRequestHandler<ChangePriorityCommand, ChangePriorityResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;
        
        // dependency injection
        public ChangePriorityCommandHandler(
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

        public async Task<ChangePriorityResponseDto> Handle(ChangePriorityCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // findIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            // checkPermission
            _permissionService.CanChangePriority(userRole, userId, incident.CreatedById);

            // changePriority
            incident.SetPriority(request.toPriority, userId);

            // addHistory
            DateTime changedAt = _timeProvider.Now();
            IncidentHistory history = IncidentHistory.AddPriorityHistory(incident.Id, userId, request.toPriority, changedAt);
            await _historyRepository.AddAsync(history, cancellationToken);

            // save UoW's job
            _unitOfWork.CommitAsync(cancellationToken);

            return new ChangePriorityResponseDto
            (
                incident.Id,
                request.toPriority.ToString(),
                userId,
                changedAt
            );
        }
    }
}