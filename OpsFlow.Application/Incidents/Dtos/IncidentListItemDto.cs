using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncidentListItemDto
    (
        string Id,
        string Title,
        string Description,
        IncidentPriority Priority,
        IncidentState State,
        DateTime CreatedAt
    );
}