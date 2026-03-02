using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(string userId, string email) : IRequest;
}