using MediatR;
using OpsFlow.Application.Tasks.Dtos;

namespace OpsFlow.Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand(int incidentId, string title, string? note) : IRequest<CreateTaskResponseDto>;
}