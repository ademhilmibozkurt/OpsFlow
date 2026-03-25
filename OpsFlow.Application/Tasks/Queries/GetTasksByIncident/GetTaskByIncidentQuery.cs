using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Queries.GetTasksByIncident
{
    public sealed record GetTasksByIncidentQuery
    (
        string incidentId,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<TaskListItemDto>>;
}