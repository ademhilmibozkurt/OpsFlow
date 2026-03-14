using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentDetail
{
    public class GetIncidentDetailQueryHandler : IRequestHandler<GetIncidentDetailQuery, IncidentDetailResponseDto>
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IPermissionService _permissionService;
        public GetIncidentDetailQueryHandler(
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser,
            IPermissionService permissionService)
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
            _permissionService = permissionService;
        }

        public async Task<IncidentDetailResponseDto> Handle(GetIncidentDetailQuery request, CancellationToken cancellationToken)
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
            _permissionService.CanGetIncidentDetail(incident.CreatedById, userId, userRole);

            // getTaskInfo
            bool isTasksDone = incident.IsAllTasksDone();

            return new IncidentDetailResponseDto
            (
                incident.Id,
                incident.Title,
                incident.Description,
                incident.Priority,
                incident.State,
                isTasksDone,
                incident.CreatedAt
            );
        }
    }
}