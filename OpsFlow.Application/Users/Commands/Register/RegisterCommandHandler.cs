using MediatR;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IUserService _userService;
        private readonly IDateTimeProvider _timeProvider;
        private readonly ITokenService _tokenService;

        public RegisterCommandHandler(
            IUserService userService,
            IDateTimeProvider timeProvider,
            ITokenService tokenService
            )
        {
            _userService = userService;
            _timeProvider = timeProvider;
            _tokenService = tokenService;
        }
        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // checkUserExisted
            AppUser? existingUser = await _userService.FindByEmailAsync(request.email);

            //  return - if existed
            if (existingUser != null)
            {
                throw new ForbiddenException("Email is already exist!");
            }

            // validateParameters

            // registerUser
            AppUser user = await _userService.CreateUserAsync(
                request.fullName,
                request.userName,
                request.email,
                request.phoneNumber,
                request.password
            );

            // generateToken
            var token = _tokenService.GenerateTokens(user);

            // returnResponseDto
            return new RegisterResponseDto
            (
                user.FullName,
                request.userName,
                request.email,
                token.AccessToken,
                token.RefreshToken,
                token.ExpiresAt
            );
        }
    }
}