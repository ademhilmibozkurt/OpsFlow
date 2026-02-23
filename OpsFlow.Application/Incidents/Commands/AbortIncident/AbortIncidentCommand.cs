using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.AbortIncident
{
    public record AbortIncidentCommand(int incidentId) : IRequest<AbortIncidentResponseDto>;
}