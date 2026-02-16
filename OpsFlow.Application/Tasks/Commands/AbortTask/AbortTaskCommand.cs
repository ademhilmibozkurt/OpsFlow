namespace OpsFlow.Application.Tasks.Commands.AbortTask
{
    public record AbortTaskCommand(int incidentId, int taskId);
}