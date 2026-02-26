using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.StartTask
{
    public record StartTaskCommand(string incidentId, string taskId) : IRequest<StartTaskResponseDto>;
}