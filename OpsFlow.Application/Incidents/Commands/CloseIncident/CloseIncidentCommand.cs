using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.CloseIncident
{
    public record CloseIncidentCommand(int incidentId) : IRequest<CloseIncidentResponseDto>;
}