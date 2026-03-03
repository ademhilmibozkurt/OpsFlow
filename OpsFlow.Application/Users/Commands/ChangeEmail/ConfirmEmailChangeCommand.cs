using MediatR;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public record ConfirmChangeEmailCommand(string userId, string newEmail, string token) : IRequest;
}