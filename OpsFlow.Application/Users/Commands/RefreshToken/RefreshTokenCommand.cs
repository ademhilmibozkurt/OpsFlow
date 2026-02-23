using MediatR;

namespace OpsFlow.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand() : IRequest;
}