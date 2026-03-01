using MediatR;
using OpsFlow.Application.Abstractions.Services;

namespace OpsFlow.Application.Users.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IUserService _userService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenService _tokenService;
        public LogoutCommandHandler(
            IUserService userService,
            IDateTimeProvider timeProvider,
            ITokenService tokenService)
        {
            _userService = userService;
            _timeProvider = timeProvider;
            _tokenService = tokenService;
        }

        public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // getCurrentUser

            // logout

            
        }
    }
}