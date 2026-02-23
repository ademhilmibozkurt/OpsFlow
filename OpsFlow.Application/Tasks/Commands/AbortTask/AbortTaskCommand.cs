using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.AbortTask
{
    public record AbortTaskCommand(int incidentId, int taskId) : IRequest<AbortTaskResponseDto>;
}