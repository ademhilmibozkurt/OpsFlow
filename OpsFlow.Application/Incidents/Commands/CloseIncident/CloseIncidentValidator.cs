using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.CloseIncident
{
    public class CloseIncidentValidator : AbstractValidator<CloseIncidentCommand>
    {
        public CloseIncidentValidator()
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