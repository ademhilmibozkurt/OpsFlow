using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Queries.GetMyTasks
{
    public sealed record GetMyTasksQuery() : IRequest<PaginatedResponseDto<TaskListItemDto>>; 
}