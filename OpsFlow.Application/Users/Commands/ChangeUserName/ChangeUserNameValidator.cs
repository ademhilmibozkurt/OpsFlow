using FluentValidation;

namespace OpsFlow.Application.Users.Commands.ChangeUserName
{
    public class ChangeUserNameValidator : AbstractValidator<ChangeUserNameCommand>
    {
        public ChangeUserNameValidator()
        {
            RuleFor(v => v.newUserName)
                .NotEmpty()
                .WithMessage("NewUserName can not null!");

            RuleFor(v => v.newUserName)
                .Must(d => d is string)
                .WithMessage("NewUserName must be string!");   

            RuleFor(v => v.newUserName)
                .Matches("^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                .WithMessage("NewUserName must be valid format!");
        }
    }
}