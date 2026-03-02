using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeUserName
{
    public record ChangeUserNameCommand(string newUserName) : IRequest<ChangeUserNameResponseDto>;
}