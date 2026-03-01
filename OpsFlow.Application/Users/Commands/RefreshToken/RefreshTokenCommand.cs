using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand(string refreshToken) : IRequest<AuthTokenResponseDto>;
}