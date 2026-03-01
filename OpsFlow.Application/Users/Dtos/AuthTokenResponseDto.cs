namespace OpsFlow.Application.Users.Dtos
{
    public sealed record AuthTokenResponseDto
    (
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );
}