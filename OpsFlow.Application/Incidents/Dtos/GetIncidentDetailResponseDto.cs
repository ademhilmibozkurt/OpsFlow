namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record GetIncidentDetailResponseDto
    (
        string IncidentId,
        string Title,
        string Description,
        string CreatedById,
        DateTime CreatedAt
    );
}