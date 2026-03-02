using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public record ChangeEmailCommand(string newEmail) : IRequest<ChangeEmailResponseDto>;
}