using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.UpdateProfile
{
    public record UpdateProfileCommand(string userName) : IRequest<UpdateProfileResponseDto>;
}
