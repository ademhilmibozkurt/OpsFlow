using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.CreateIncident
{
    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, CreateIncidentResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IIncidentHistoryRepository _historyRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        // dependency injection
        public CreateIncidentCommandHandler(
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

        public async Task<CreateIncidentResponseDto> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // checkPermission
            _permissionService.CanCreateIncident(userRole);

            // createIncident
            Incident incident =  Incident.Create(
                request.title,
                request.description,
                userId
            );

            DateTime createdAt = _timeProvider.Now();
            incident.CreatedAt = createdAt;

            await _incidentRepository.AddAsync(incident, cancellationToken);

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(
                incident.Id,
                userId,
                IncidentState.Open,
                createdAt);
            
            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync(cancellationToken);

            return new CreateIncidentResponseDto
            (
                incident.Id,
                request.title,
                request.description,
                userId,
                createdAt
            );
        }
    }
}