using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentDetail
{
    public record GetIncidentDetailCommand(string incidentId) : IRequest<IncidentDetailResponseDto>;
}