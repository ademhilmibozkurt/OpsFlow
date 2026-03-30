namespace OpsFlow.Application.Users.Dtos
{
    public sealed record UserActivityItemDto
    (
        string UserId,
        string FullName,
        string UserName,
        Enum EventType,
        DateTime DoneAt,
        string EventIncidentId,
        string? EventTaskId
    );
}