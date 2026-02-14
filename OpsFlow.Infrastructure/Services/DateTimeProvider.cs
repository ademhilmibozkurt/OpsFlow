using OpsFlow.Application.Abstractions.Services;

namespace OpsFlow.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}