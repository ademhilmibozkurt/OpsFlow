using MediatR;

namespace OpsFlow.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(string userId) : IRequest;
}