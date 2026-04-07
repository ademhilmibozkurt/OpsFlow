using FluentValidation;

namespace OpsFlow.Application.Users.Commands.ChangeEmail
{
    public class RequestEmailChangeValidator : AbstractValidator<RequestEmailChangeCommand>
    {
        public RequestEmailChangeValidator()
        {
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