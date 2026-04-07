using FluentValidation;

namespace OpsFlow.Application.Users.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
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