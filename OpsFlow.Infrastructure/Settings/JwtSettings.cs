namespace OpsFlow.Infrastructure.Settings
{
    public sealed class JwtSettings
    {
        public string Secret {get; init;} = default;
        public string Issuer {get; init;} = default;
        public string Audience {get; init;} = default;
    }
}