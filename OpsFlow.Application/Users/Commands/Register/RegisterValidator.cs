using FluentValidation;

namespace OpsFlow.Application.Users.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(v => v.fullName)
                .NotEmpty()
                .WithMessage("FullName can not null!");

            RuleFor(v => v.fullName)
                .Must(d => d is string)
                .WithMessage("FullName must be string type!");

            RuleFor(v => v.fullName)
                .Matches("^([A-Z][a-z]{1,} )([A-Z][a-z]{1,} )?([A-Z][a-z]{1,})$")
                .WithMessage("FullName must be at least two words, starts with capital letter, not include alphanumeric and/or numeric values!");
            
            RuleFor(v => v.email)
                .NotEmpty()
                .WithMessage("New Email can not null!");

            RuleFor(v => v.email)
                .EmailAddress()
                .WithMessage("New email must be Email Address type!");
            
            RuleFor(v => v.email)
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")
                .WithMessage("New Email must be valid email format!");

            RuleFor(v => v.phoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber can not null!");

            RuleFor(v => v.phoneNumber)
                .Must(d => d is string)
                .WithMessage("PhoneNumber must be string!");
            
            RuleFor(v => v.phoneNumber)
                .Matches("^[0-9]{3}[0-9]{3}[0-9]{4}$")
                .WithMessage("PhoneNumber must be ten characters, only include numbers, not include alphanumeric characters!");

            RuleFor(v => v.userName)
                .NotEmpty()
                .WithMessage("UserName can not null!");

            RuleFor(v => v.userName)
                .Must(d => d is string)
                .WithMessage("UserName must be string!");   

            RuleFor(v => v.userName)
                .Matches("^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$")
                .WithMessage("UserName must be valid format!");

            RuleFor(v => v.password)
                .NotEmpty()
                .WithMessage("Password can not null!");    

            RuleFor(v => v.password)
                .Must(d => d is string)
                .WithMessage("Password must be string!");    

            RuleFor(v => v.password)
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("Password must be valid format!");
        }
    }
}