using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Commands.CreateIncident
{
    public record CreateIncidentCommand(string title, string description) : IRequest<CreateIncidentResponseDto>;
}