namespace OpsFlow.Application.Models
{
    public record RefreshTokenModel
    {
        public string Token {get; set;}
        public string UserId {get; set;}
        public DateTime ExpiresAt {get; set;}
        public bool IsRevoked {get; set;} = false;
    }
}