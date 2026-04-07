using FluentValidation;

namespace OpsFlow.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator()
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

            RuleFor(v => v.phoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber can not null!");

            RuleFor(v => v.phoneNumber)
                .Must(d => d is string)
                .WithMessage("PhoneNumber must be string!");
            
            RuleFor(v => v.phoneNumber)
                .Matches("^[0-9]{3}[0-9]{3}[0-9]{4}$")
                .WithMessage("PhoneNumber must be ten characters, only include numbers, not include alphanumeric characters!");
        }
    }
}