using FluentValidation;

namespace OpsFlow.Application.Users.Commands.Logout
{
    public class LogoutValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutValidator()
        {
            RuleFor(v => v.refreshToken)
                .NotEmpty()
                .WithMessage("RefreshToken can not null!");

            RuleFor(v => v.refreshToken)
                .Must(d => d is string)
                .WithMessage("RefreshToken must be string!");
        }
    }
}