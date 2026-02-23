using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.CloseTask
{
    public record CloseTaskCommand(int incidentId, int taskId) : IRequest<CloseTaskResponseDto>;
}