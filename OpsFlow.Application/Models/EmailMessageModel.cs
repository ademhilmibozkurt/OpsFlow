namespace OpsFlow.Application.Models
{
    public record EmailMessageModel
    {
        public required string To {get; init;} 
        public required string Subject {get; init;}
        public required string Body {get; init;}
    }
}