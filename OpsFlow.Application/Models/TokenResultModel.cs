namespace OpsFlow.Application.Models
{
    public record TokenResultModel
    (
        string AccessToken,
        string RefreshToken,
        DateTime ExpiresAt
    );
}