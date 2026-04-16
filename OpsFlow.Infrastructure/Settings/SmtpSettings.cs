namespace OpsFlow.Infrastructure.Settings
{
    public sealed class SmtpSettings
    {
        public required string Host { get; init; }
        public int Port { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
        public required string From { get; init; }
    }
}