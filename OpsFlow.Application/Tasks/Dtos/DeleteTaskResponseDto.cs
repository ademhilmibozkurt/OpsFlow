namespace OpsFlow.Application.Tasks.Dtos
{
    public sealed record DeleteTaskResponseDto
    (
        string TaskId,
        string DeletedById
    );
}