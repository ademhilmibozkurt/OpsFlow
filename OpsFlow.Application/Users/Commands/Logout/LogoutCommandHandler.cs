using MediatR;

namespace OpsFlow.Application.Users.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        public LogoutCommandHandler(Parameters)
        {
            
        }

        public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}