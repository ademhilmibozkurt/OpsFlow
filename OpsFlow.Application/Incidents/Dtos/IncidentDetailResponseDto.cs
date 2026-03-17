using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record IncidentDetailResponseDto
    (
        string Title,
        string Description,
        IncidentPriority CurrentPriority,
        IncidentState CurrentState,
        bool IsAllTasksDone,
        int TaskCount,
        int OpenTaskCount,
        int CompletedTaskCount,
        string CreatedBy,
        DateTime CreatedAt
    );
}