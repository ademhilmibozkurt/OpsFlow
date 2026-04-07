using MediatR;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public record ConfirmEmailChangeCommand(string userId, string newEmail, string token) : IRequest;
}