using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.AbortIncident
{
    public class AbortIncidentValidator : AbstractValidator<AbortIncidentCommand>
    {
        public AbortIncidentValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");
        }
    }
}