using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentTimeline
{
    public class GetIncidentTimelineQueryHandler : IRequestHandler<GetIncidentTimelineQuery, IncidentTimelineResponseDto>
    {
        public Task<IncidentTimelineResponseDto> Handle(GetIncidentTimelineQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}