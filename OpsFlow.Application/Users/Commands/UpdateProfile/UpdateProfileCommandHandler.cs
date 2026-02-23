using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileResponseDto>
    {
        public UpdateProfileCommandHandler(Parameters)
        {
            
        }
        
        public Task<UpdateProfileResponseDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}