using FluentValidation;

namespace OpsFlow.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(v => v.currentPassword)
                .NotEmpty()
                .WithMessage("CurrentPassword can not null!");

            RuleFor(v => v.currentPassword)
                .Must(d => d is string)
                .WithMessage("CurrentPassword must be string!");

            RuleFor(v => v.newPassword)
                .NotEmpty()
                .WithMessage("NewPassword can not null!");    

            RuleFor(v => v.newPassword)
                .Must(d => d is string)
                .WithMessage("NewPassword must be string!");    

            RuleFor(v => v.newPassword)
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("NewPassword must be valid format!");
        }
    }
}