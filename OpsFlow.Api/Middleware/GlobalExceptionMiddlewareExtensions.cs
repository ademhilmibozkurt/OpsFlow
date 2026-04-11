namespace OpsFlow.Api.Middleware
{
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware
        (
            this IApplicationBuilder app
        )
        {
            return app.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}