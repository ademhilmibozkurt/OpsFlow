namespace OpsFlow.Api.Contracts
{
    public class ErrorResponse
    {
        public bool Success {get; init;} = false;
        public int StatusCode {get; init;}
        public string Message {get; init;} = string.Empty;
        public IEnumerable<string>? Error {get; init;}
        public DateTime TimeStamp {get; init;} = DateTime.UtcNow;
    }
}