namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncidentListItemDto
    (
        string Title,
        IncidentPriority Priority,
        IncidentState State,
        DateTime CreatedAt
    );
}