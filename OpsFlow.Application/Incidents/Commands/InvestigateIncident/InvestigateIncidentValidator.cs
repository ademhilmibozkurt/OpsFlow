using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.InvestigateIncident
{
    public class InvestigateIncidentValidator : AbstractValidator<InvestigateIncidentCommand>
    {
        public InvestigateIncidentValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");

            RuleFor(v => v.incidentId)
                .Must(d => d is string)
                .WithMessage("IncidentId must be string type!");
        }
    }
}