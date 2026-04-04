using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.InvestigateIncident
{
    public class InvestigateIncidentValidator : AbstractValidator<InvestigateIncidentCommand>
    {
        public InvestigateIncidentValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .Must(d => d is string)
                .WithMessage("IncidentId can not null. Must be string type!");
        }
    }
}