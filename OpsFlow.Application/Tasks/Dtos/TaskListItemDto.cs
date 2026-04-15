using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record TaskListItemDto
    (
        string TaskId,
        string IncidentId,
        string CreatedById,
        string? AssigneeId,
        string Title,
        string Note,
        IncidentTaskState State,
        DateTime CreatedAt,
        string? AbortionNote
    );
}