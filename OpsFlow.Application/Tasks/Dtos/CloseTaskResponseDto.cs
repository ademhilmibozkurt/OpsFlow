namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record CloseTaskResponseDto
    (
        string TaskId,
        string Title,
        string ClosedById,
        DateTime OccuredAt
    );
}