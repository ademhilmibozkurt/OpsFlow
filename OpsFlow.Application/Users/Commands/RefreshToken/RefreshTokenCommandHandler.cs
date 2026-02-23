using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand>
    {
        public RefreshTokenCommandHandler(Parameters)
        {
            
        }

        Task IRequestHandler<RefreshTokenCommand>.Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}