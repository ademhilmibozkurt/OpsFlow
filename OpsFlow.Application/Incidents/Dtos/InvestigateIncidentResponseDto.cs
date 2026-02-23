namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record InvestigateIncidentResponseDto
    (
        string IncidentId,
        string InvestigatedById,
        DateTime OccuredAt
    );
}