using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Infrastructure.Identity;

namespace OpsFlow.Application.Users.Commands.Register
{
    public class RegisterCommandHandler
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
            // validateValues

            // createUser
            AppUser user = new AppUser
            {
                UserName = command.userName,
                Email    = command.email,
                PhoneNumber = command.phoneNumber,
                Role = AppRole.User,
                CreatedAt = _timeProvider.Now()
            };

            // addUser
            await _userRepository.AddAsync(user);

            // save
            _unitOfWork.Commit();
        }
    }
}