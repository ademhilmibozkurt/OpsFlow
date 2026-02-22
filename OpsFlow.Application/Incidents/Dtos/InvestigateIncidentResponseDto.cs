namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncestigateIncidentResponseDto
    (
        string IncidentId,
        string InvestigatedById,
        DateTime OccuredAt
    );
}