using MediatR;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public record RequestChangeEmailCommand(string newEmail) : IRequest;
}