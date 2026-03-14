using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentDetail
{
    public record GetIncidentDetailQuery(string incidentId) : IRequest<IncidentDetailResponseDto>;
}