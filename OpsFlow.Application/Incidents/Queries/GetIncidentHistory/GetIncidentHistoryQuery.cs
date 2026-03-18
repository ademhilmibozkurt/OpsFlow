using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentHistory
{
    public sealed record GetIncidentHistoryQuery(string incidentId) : IRequest<IncidentHistoryResponseDto>; 
}