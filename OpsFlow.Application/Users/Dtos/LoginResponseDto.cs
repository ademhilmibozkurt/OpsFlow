namespace OpsFlow.Application.Users.Dtos
{
    public sealed record LoginResponseDto
    (
        Guid UserId,
        string FullName,
        string UserName,
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );
}