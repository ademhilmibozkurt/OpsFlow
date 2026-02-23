using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(string password) : IRequest<ChangePasswordResponseDto>;
}