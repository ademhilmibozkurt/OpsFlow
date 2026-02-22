namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record StartTaskResponseDto
    (
        string TaskId,
        string Title,
        string StartedById,
        DateTime OccuredAt
    );
}