namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record AbortTaskResponseDto
    (
        string TaskId,
        string Title,
        string AbortedById,
        string AbortionNote,
        DateTime OccuredAt
    );
}