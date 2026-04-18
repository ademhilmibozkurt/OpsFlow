using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Results;
using OpsFlow.Application.Models;

namespace OpsFlow.Application.Users.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly ITokenRepository _tokenRepository;
        public LogoutCommandHandler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // getResult
            Result<RefreshTokenModel> result = await _tokenRepository.GetByTokenAsync
            (
                request.refreshToken,
                cancellationToken
            );

            // getToken
            RefreshTokenModel token = result.Value!;

            // logout
            if (token != null)
                await _tokenRepository.RevokeAsync
                (
                    token.Token,
                    cancellationToken
                );
        }
    }
}