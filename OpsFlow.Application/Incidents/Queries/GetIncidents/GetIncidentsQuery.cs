using OpsFlow.Application.Incidents.Dtos;

namespace OpsFlow.Application.Incidents.Queries.GetIncidents
{
    public record GetIncidentsQuery 
    (
        IncidentState State,
        IncidentPriority Priority,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<IncidentListItemDto>>;
}