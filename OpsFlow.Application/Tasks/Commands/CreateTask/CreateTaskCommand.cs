namespace OpsFlow.Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand(int incidentId, string title, string? note);
}