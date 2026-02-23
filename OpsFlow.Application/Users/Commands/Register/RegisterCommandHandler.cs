using MediatR;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IDateTimeProvider timeProvider,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _timeProvider   = timeProvider;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RegisterCommand command)
        {
            // createUser
            AppUser user = new AppUser
            {
                FullName = command.fullName,
                UserName = command.userName,
                Password 
                Email    = command.email,
                PhoneNumber = command.phoneNumber,
                CreatedAt = _timeProvider.Now()
            };

            // addUser
            await _userRepository.AddAsync(user);

            // save
            _unitOfWork.Commit();
        }
    }
}