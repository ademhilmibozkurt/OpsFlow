using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.Login
{
    public record LoginCommand(string email, string password) : IRequest<AuthTokenResponseDto>;
}