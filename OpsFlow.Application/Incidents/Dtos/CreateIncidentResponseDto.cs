namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record CreateIncidentResponseDto
    (
        string IncidentId,
        string Title,
        string Description,
        string CreatedById,
        DateTime CreatedAt
    );
}