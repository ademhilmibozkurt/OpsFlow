namespace OpsFlow.Application.Users.Dtos
{
    public sealed record RegisterResponseDto
    (
        Guid UserId,
        string FullName,
        string UserName,
        string Email,
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );
}