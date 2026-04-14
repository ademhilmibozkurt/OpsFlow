namespace OpsFlow.Application.Models
{
    public record RefreshTokenModel
    {
        public required string Token {get; set;}
        public required string UserId {get; set;}
        public DateTime ExpiresAt {get; set;}
        public bool IsRevoked {get; set;} = false;
    }
}