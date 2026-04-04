using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.CreateIncident
{
    public class CreateIncidentValidator : AbstractValidator<CreateIncidentCommand>
    {
        public CreateIncidentValidator()
        {
            RuleFor(v => v.title)
                .NotEmpty()
                .Must(d => d is string)
                .Length(12)
                .WithMessage("Title can not null. Must be string type!");

            RuleFor(v => v.description)
                .NotEmpty()
                .Must(d => d is string)
                .Length(20)
                .WithMessage("Description can not null and must be string!");
        }
    }
}