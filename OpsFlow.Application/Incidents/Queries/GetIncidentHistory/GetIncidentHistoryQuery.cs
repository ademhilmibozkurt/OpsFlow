using MediatR;
using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentHistory
{
    public sealed record GetIncidentHistoryQuery
    (
        string incidentId,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<HistoryItemDto>>; 
}