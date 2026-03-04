namespace OpsFlow.Application.Models
{
    public record EmailMessageModel
    {
        public string To {get; init;}
        public string Subject {get; init;}
        public string Body {get; init;}
    }
}