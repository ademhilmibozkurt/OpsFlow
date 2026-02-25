using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.AbortIncident
{
    public record AbortIncidentCommand(string incidentId, string abortionNote) : IRequest<AbortIncidentResponseDto>;
}