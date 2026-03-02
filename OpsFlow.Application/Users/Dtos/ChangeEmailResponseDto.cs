namespace OpsFlow.Application.Users.Dtos
{
    public sealed record ChangeEmailResponseDto
    (
        string FullName,
        string Email,
        DateTime ChangedAt
    );
}