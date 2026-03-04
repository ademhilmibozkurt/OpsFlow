using OpsFlow.Application.Models;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessageModel message, CancellationToken cancellationToken);
    }
}