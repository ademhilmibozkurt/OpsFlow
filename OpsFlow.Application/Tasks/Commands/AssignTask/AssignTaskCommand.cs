namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public record AssignTaskCommand(int incidentId, int taskId, int userId);
}