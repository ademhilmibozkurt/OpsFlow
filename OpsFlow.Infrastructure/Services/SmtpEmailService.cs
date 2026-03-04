using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Models;
using OpsFlow.Infrastructure.Settings;

namespace OpsFlow.Infrastructure.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        public SmtpEmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(EmailMessageModel message, CancellationToken cancellationToken)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential
                (
                    _settings.UserName,
                    _settings.Password
                ),
                EnableSsl = true
            };

            MailMessage mail = new MailMessage
            (
                _settings.From,
                message.To,
                message.Subject,
                message.Body
            );

            await client.SendMailAsync(mail, cancellationToken);
        }
    }
}