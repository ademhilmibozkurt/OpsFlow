using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Queries.GetTaskDetail
{
    public record GetTaskDetailQuery(string taskId) : IRequest<TaskDetailResponseDto>;
}