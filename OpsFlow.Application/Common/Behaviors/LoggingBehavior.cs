using System.Diagnostics;
using MediatR;

namespace OpsFlow.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior
        (
            ILogger<LoggingBehavior<TRequest, TResponse>> logger
        )
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            Stopwatch stopwatch = Stopwatch.StartNew();

            _logger.LogInformation
            (
                "Handling {RequestName} {@Request}",
                requestName,
                request
            );

            try
            {
                var response = await next();
                stopwatch.Stop();
                _logger.LogInformation
                (
                    "Handled {RequestName} in {ElapsedMilliseconds} ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds
                );

                return response;
            }
            catch(Exception e)
            {
                stopwatch.Stop();
                _logger.LogError
                (
                    e,
                    "Error handling {RequestName} after {ElapsedMilliseconds} ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds
                );

                throw;
            }
        }
    }
}