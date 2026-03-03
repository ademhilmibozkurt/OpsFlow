namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record TaskDetailResponseDto
    (
        string TaskId,
        string IncidentId,
        string Title,
        string Note,
        string? AbortionNote,
        string IncidentTitle,
        string CreatedById,
        string? AssigneeId,
        DateTime CreatedAt
    );
}