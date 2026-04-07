using FluentValidation;

namespace OpsFlow.Application.Users.Commands.DeleteUser
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(v => v.userId)
                .NotEmpty()
                .WithMessage("UserId can not null!");

            RuleFor(v => v.userId)
                .Must(d => d is string)
                .WithMessage("UserId must be string!");
        }
    }
}