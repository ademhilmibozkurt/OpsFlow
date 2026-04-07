using FluentValidation;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public class ConfirmEmailChangeValidator : AbstractValidator<ConfirmEmailChangeCommand>
    {
        public ConfirmEmailChangeValidator()
        {
            RuleFor(v => v.userId)
                .NotEmpty()
                .WithMessage("UserId can not null!");

            RuleFor(v => v.userId)
                .Must(d => d is string)
                .WithMessage("UserId must be string!");

            RuleFor(v => v.token)
                .NotEmpty()
                .WithMessage("Token can not null!");    

            RuleFor(v => v.token)
                .Must(d => d is string)
                .WithMessage("Token must be string!");    

            RuleFor(v => v.newEmail)
                .NotEmpty()
                .WithMessage("New Email can not null!");

            RuleFor(v => v.newEmail)
                .EmailAddress()
                .WithMessage("New email must be Email Address type!");
            
            RuleFor(v => v.newEmail)
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                .WithMessage("New Email must be valid email format!");
        }
    }
}