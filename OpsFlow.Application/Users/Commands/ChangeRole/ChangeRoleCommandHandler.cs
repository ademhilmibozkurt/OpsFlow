using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.ChangeRole
{
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, ChangeRoleResponseDto>
    {
        public ChangeRoleCommandHandler(Parameters)
        {
            
        }
        
        public Task<ChangeRoleResponseDto> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}