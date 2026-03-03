using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
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
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // chechPermission
            _permissions.CanAbortIncident(userRole);

            // validateParameters

            // abortIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            incident.Abort(request.abortionNote, userId);

            // createTimestamp
            DateTime abortedAt = _timeProvider.Now();

            // addHistory
            IncidentHistory history = IncidentHistory.AddIncidentHistory(
                request.incidentId, 
                _currentUser.UserId, 
                IncidentState.Aborted, 
                abortedAt,
                request.abortionNote);

            await _historyRepository.AddAsync(history, cancellationToken);

            // save
            _unitOfWork.CommitAsync(cancellationToken);

            
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