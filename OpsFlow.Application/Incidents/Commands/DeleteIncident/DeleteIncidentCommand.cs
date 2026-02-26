using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.DeleteIncident
{
    public record DeleteIncidentCommand(string incidentId) : IRequest<DeleteIncidentResponseDto>;
}