using MediatR;

namespace OpsFlow.Application.Users.Commands.Logout
{
    public record LogoutCommand() : IRequest;
}
