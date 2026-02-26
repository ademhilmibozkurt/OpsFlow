using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.InvestigateIncident
{
    public record InvestigateIncidentCommand(string incidentId) : IRequest<InvestigateIncidentResponseDto>;
}