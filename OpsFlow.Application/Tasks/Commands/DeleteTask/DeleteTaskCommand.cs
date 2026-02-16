namespace OpsFlow.Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(int incidentId, int taskId);
}