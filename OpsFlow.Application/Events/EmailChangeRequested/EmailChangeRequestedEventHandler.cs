using MediatR;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Models;

namespace OpsFlow.Application.Events.EmailChangeRequested
{
    public sealed class EmailChangeRequestedEventHandler : INotificationHandler<EmailChangeRequestedEvent>
    {
        private readonly IEmailService _emailService;
        public EmailChangeRequestedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Handle(EmailChangeRequestedEvent notification, CancellationToken cancellationToken)
        {
            EmailMessageModel model = new EmailMessageModel
            {
                To = notification.NewEmail,
                Subject = "Confirm your new email!",
                Body = $"Click to confirm your email {notification.ConfirmLink}"
            };

            await _emailService.SendAsync(model, cancellationToken);
        }
    }
}