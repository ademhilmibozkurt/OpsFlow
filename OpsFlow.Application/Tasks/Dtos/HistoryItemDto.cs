namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record HistoryItemDto
    (
        string TaskId,
        string IncidentId,
        string PerformedById,
        string Title,
        string? Note,
        Enum EventType,
        DateTime OccuredAt
    );
}