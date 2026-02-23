using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(int incidentId, int taskId) : IRequest<DeleteTaskResponseDto>;
}