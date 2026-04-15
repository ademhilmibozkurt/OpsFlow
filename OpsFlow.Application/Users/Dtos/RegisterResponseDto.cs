namespace OpsFlow.Application.Users.Dtos
{
    public sealed record RegisterResponseDto
    (
        string FullName,
        string UserName,
        string Email,
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );
}