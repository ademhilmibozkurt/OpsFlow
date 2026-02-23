using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public record AssignTaskCommand(int incidentId, int taskId, int userId) : IRequest<AssignTaskResponseDto>;
}