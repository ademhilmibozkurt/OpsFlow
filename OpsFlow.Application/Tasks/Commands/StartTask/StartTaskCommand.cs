using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.StartTask
{
    public record StartTaskCommand(int incidentId, int taskId) : IRequest<StartTaskResponseDto>;
}