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

        public GetIncidentDetailQueryHandler
        (
            IIncidentRepository incidentRepository,
            ICurrentUserService currentUser
        )
        {
            _incidentRepository = incidentRepository;
            _currentUser = currentUser;
        }

        public async Task<IncidentDetailResponseDto> Handle(GetIncidentDetailQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");
            string userRole = _currentUser.Role ?? throw new AuthenticationException("User not authenticated!");

            // checkPermission - everyone should get incident detail

            // getIncident
            Incident incident = await _incidentRepository.GetByIdAsync(
                request.incidentId,
                cancellationToken)
                ?? throw new NotFoundException("Incident not found!");

            // isAllTaskDone
            bool isAllTaskDone = incident.IsAllTasksDone();

            // taskCount
            int taskCount = incident.TaskCount();

            // openTaskCount
            int openTaskCount = incident.OpenTaskCount();

            // completedTaskCount
            int completedTaskCount = incident.CompletedTaskCount();

            // returnDto
            return new IncidentDetailResponseDto
            (
                incident.Title,
                incident.Description,
                incident.Priority,
                incident.State,
                isAllTaskDone,
                taskCount,
                openTaskCount,
                completedTaskCount,
                incident.CreatedById,
                incident.CreatedAt
            );
        }
    }
}