namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record CloseIncidentResponseDto
    (
        string IncidentId,
        string ClosedById,
        DateTime OccuredAt
    );
}