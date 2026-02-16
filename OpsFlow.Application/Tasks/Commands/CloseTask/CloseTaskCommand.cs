namespace OpsFlow.Application.Tasks.Commands.CloseTask
{
    public record CloseTaskCommand(int incidentId, int taskId);
}