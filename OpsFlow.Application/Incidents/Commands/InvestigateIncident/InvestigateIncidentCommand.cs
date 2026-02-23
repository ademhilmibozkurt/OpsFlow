using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.InvestigateIncident
{
    public record InvestigateIncidentCommand(int incidentId) : IRequest<IncestigateIncidentResponseDto>;
}