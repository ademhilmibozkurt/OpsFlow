using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.AbortTask
{
    public record AbortTaskCommand(string incidentId, string taskId, string abortionNote) : IRequest<AbortTaskResponseDto>;
}