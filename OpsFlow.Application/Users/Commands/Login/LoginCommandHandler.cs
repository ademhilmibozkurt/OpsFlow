using System.Security.Principal;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Models;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(
            IUserService userService,
            IDateTimeProvider timeProvider,
            ITokenService tokenService)
        {
            _userService = userService;
            _timeProvider = timeProvider;
            _tokenService = tokenService;
        }
        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
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

            return new LoginResponseDto
            (
                user.Id,
                user.FullName,
                user.UserName,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt
            );
        }
    }
}