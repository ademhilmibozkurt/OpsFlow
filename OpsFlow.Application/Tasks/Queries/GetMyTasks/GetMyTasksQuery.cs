using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Queries.GetMyTasks
{
    public sealed record GetMyTasksQuery
    (
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<TaskListItemDto>>; 
}