using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.Register
{
    public record RegisterCommand(string fullName, string userName, string email, string phoneNumber, string password) : IRequest<RegisterResponseDto>;
}