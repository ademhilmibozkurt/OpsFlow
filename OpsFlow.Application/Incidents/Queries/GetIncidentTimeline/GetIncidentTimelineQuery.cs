using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentTimeline
{
    public sealed record GetIncidentTimelineQuery(string incidentId) : IRequest<IncidentTimelineResponseDto>;
}