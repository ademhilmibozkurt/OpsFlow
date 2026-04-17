using FluentValidation;
using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Commands.ChangeRole
{
    public class ChangeRoleValidator : AbstractValidator<ChangeRoleCommand>
    {
        public ChangeRoleValidator()
        {
            RuleFor(v => v.userId)
                .NotEmpty()
                .WithMessage("UserId can not null!");

            RuleFor(v => v.userId)
                .Must(d => d is string)
                .WithMessage("UserId must be string!");

            RuleFor(v => v.userRole)
                .NotEmpty()
                .WithMessage("UserRole can not null!");    

            RuleFor(v => v.userRole)
                .Must(d => d is string)
                .WithMessage("UserRole must be AppRole type!");
        }
    }
}