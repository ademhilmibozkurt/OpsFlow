using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Models;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthTokenResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;
        public LoginCommandHandler(
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
        public async Task<AuthTokenResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // findUser
            AppUser user = await _userService.FindByEmailAsync(
                request.email)
                ?? throw new NotFoundException("User not found via email!");

            // checkPassword
            bool validPassword = await _userService.CheckPasswordAsync(
                user,
                request.password)
                ? false: throw new IncorrectCredentialsException("User credentials incorrect!");

            // generateToken
            TokenResultModel token = _tokenService.GenerateTokens(user);

            await _tokenRepository.AddAsync(
                token.RefreshToken,
                user.Id,
                _timeProvider.Now().AddDays(30),
                cancellationToken);

            return new AuthTokenResponseDto
            (
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt
            );
        }
    }
}