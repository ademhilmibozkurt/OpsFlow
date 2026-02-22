namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record GetTaskDetailResponseDto
    (
        string TaskId,
        string IncidentId,
        string Title,
        string Note,
        string CreatedById,
        DateTime CreatedAt
    );
}