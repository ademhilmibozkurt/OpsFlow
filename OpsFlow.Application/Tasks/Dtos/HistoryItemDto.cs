namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record HistoryItemDto
    (
        string taskId,
        string PerformedById,
        string Title,
        string? Note,
        Enum EventType,
        DateTime OccuredAt
    );
}