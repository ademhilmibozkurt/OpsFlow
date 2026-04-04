using FluentValidation;

namespace OpsFlow.Application.Incidents.Commands.DeleteIncident
{
    public class DeleteIncidentValidator : AbstractValidator<DeleteIncidentCommand>
    {
        public DeleteIncidentValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .Must(d => d is string)
                .WithMessage("IncidentId can not null. Must be string type!");
        }
    }
}