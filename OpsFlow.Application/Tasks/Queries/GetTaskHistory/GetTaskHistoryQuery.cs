using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Queries.GetTaskHistory
{
    public sealed record GetTaskHistoryQuery
    (
        string taskId,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<HistoryItemDto>>; 
}