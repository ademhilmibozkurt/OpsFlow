namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncidentHistoryResponseDto
    (
        string IncidentId,
        string PerformedById,
        Enum EventType,
        DateTime OccuredAt
    );
}