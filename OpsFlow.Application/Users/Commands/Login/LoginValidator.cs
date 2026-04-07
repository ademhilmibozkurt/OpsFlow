using FluentValidation;

namespace OpsFlow.Application.Users.Commands.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(v => v.email)
                .NotEmpty()
                .WithMessage("Email can not null!");

            RuleFor(v => v.email)
                .EmailAddress()
                .WithMessage("Email must be Email Address type!");
            
            RuleFor(v => v.email)
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                .WithMessage("Email must be valid email format!");

            RuleFor(v => v.password)
                .NotEmpty()
                .WithMessage("Password can not null!");    

            RuleFor(v => v.password)
                .Must(d => d is string)
                .WithMessage("NewPassword must be string!");    

            RuleFor(v => v.password)
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("NewPassword must be valid format!");
        }
    }
}