using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Common.Results;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Models;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthTokenResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;
        public RefreshTokenCommandHandler(
            IUserService userService,
            IDateTimeProvider timeProvider,
            ITokenService tokenService,
            ITokenRepository tokenRepository)
        {
            _userService = userService;
            _timeProvider = timeProvider;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository; 
        }

        public async Task<AuthTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // getResult
            RefreshTokenModel existingToken = await _tokenRepository.GetByTokenAsync
            (
                request.refreshToken,
                cancellationToken
            );

            // checkRefreshToken - is revoked or expired
            if(existingToken.IsRevoked || existingToken.ExpiresAt < _timeProvider.Now())
                throw new InvalidRefreshTokenException();

            // makeRevoked
            await _tokenRepository.RevokeAsync(existingToken.Token, cancellationToken);

            // findUser
            AppUser user = await _userService.FindByIdAsync(
                existingToken.UserId)
                ?? throw new NotFoundException("User not found!");

            // generateNewTokens
            TokenResultModel token = _tokenService.GenerateTokens(user);

            // addTokens
            await _tokenRepository.AddAsync(token.RefreshToken, user.Id, _timeProvider.Now().AddDays(30), cancellationToken);

            // returnDto
            return new AuthTokenResponseDto
            (
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt
            );
        }
    }
}