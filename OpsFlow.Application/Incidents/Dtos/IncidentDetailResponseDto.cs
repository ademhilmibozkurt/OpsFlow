using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncidentDetailResponseDto
    (
        string IncidentId,
        string Title,
        string Description,
        IncidentPriority Priority,
        IncidentState CurrentState,
        bool IsAllTasksDone,
        DateTime CreatedAt
    );
}