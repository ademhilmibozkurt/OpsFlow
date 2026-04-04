using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.CloseIncident
{
    public class CloseIncidentValidator : AbstractValidator<CloseIncidentCommand>
    {
        public CloseIncidentValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .Must(d => d is string)
                .WithMessage("IncidentId can not null. Must be string type!");
        }
    }
}