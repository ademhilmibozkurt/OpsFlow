using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(string incidentId, string taskId) : IRequest<DeleteTaskResponseDto>;
}