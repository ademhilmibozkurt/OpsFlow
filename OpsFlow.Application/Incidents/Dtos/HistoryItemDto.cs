namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record HistoryItemDto
    (
        string IncidentId,
        string PerformedById,
        Enum EventType,
        DateTime OccuredAt,
        string? Note
    );
}