using MediatR;
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
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(
            IUserService userService,
            ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
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

            return new AuthTokenResponseDto
            (
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt
            );
        }
    }
}