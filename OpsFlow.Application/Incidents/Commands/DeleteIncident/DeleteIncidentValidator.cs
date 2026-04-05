using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.DeleteIncident
{
    public class DeleteIncidentValidator : AbstractValidator<DeleteIncidentCommand>
    {
        public DeleteIncidentValidator()
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