using MediatR;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeRole
{
    public record ChangeRoleCommand(string userId, string userRole) : IRequest<ChangeRoleResponseDto>;
}