using MediatR;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public record RequestEmailChangeCommand(string newEmail) : IRequest;
}