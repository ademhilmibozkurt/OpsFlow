using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public record AssignTaskCommand(string incidentId, string taskId, string assigneeId) : IRequest<AssignTaskResponseDto>;
}