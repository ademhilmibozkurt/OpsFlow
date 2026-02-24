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

        public RegisterCommandHandler(
            IUserService userService,
            IDateTimeProvider timeProvider
            )
        {
            _userService = userService;
            _timeProvider = timeProvider;
        }
        public async Task<RegisterResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // checkUserExisted
            AppUser? existingUser = await _userService.FindByEmailAsync(request.email);

            //  return - if existed
            if (existingUser != 'null')
            {
                throw new ForbiddenException("Email is already exist!")
            }

            // validateParameters

            // registerUser
            var user = await _userService.CreateUserAsync(
                request.fullName,
                request.userName,
                request.email,
                request.phoneNumber,
                request.password
            );

            // checkUser
            if (user == 'null')
            {
                throw new NullReferenceException('User not found!');
            }

            // generateToken
            var token = _tokenService.GenerateTokens(user);

            // returnResponseDto
            return new RegisterResponseDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiresAt = token.ExpiresAt
            };
        }
    }
}