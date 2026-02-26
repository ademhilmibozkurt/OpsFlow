using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.CloseTask
{
    public record CloseTaskCommand(string incidentId, string taskId) : IRequest<CloseTaskResponseDto>;
}