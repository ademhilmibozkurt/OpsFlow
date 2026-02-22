namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record AbortIncidentResponseDto
    (
        string IncidentId,
        string AbortionNote,
        string AbortedById,
        DateTime OccuredAt
    );
}