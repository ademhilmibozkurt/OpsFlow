using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.CreateIncident
{
    public class CreateIncidentValidator : AbstractValidator<CreateIncidentCommand>
    {
        public CreateIncidentValidator()
        {
            RuleFor(v => v.title)
                .NotEmpty()
                .WithMessage("Title can not null!");

            RuleFor(v => v.title)
                .Must(d => d is string)
                .WithMessage("Title must be string type!");

            RuleFor(v => v.title)
                .Matches("(\b\\w+\b.*){2}")
                .WithMessage("Title must at least two words!");

            RuleFor(v => v.description)
                .NotEmpty()
                .WithMessage("Description can not null!");

            RuleFor(v => v.description)
                .Must(d => d is string)
                .WithMessage("Description must be string!");
            
            RuleFor(v => v.description)
                .Matches("(\b\\w+\b.*){3}")
                .WithMessage("Description must at least three words!");
        }
    }
}