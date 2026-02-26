namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record AssignTaskResponseDto
    (
        string TaskId,
        string Title,
        string AssignedById,
        string AssigneeId,
        DateTime OccuredAt
    );
}