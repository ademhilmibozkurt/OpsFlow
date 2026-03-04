using MediatR;

namespace OpsFlow.Application.Events.EmailChangeRequested
{
    public sealed class EmailChangeRequestedEvent : INotification
    {
        public string UserId {get;}
        public string NewEmail {get;}
        public string ConfirmLink {get;}

        public EmailChangeRequestedEvent(
            string userId,
            string newEmail,
            string confirmLink
        )
        {
            UserId = userId;
            NewEmail = newEmail;
            ConfirmLink = confirmLink;
        }

    }
}