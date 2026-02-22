namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record DeleteIncidentResponseDto
    (
        string IncidentId,
        string DeletedById
    );
}