using MediatR;
using OpsFlow.Application.Incidents.Dtos;
using OpsFlow.Domain.Enums;

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