using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Queries.GetTaskDetail
{
    public record GetTaskDetailCommand(string taskId, string incidentId) : IRequest<TaskDetailResponseDto>;
}