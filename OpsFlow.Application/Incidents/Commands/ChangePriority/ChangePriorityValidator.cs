using FluentValidation;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Commands.ChangePriority
{
    public class ChangePriorityValidator : AbstractValidator<ChangePriorityCommand>
    {
        public ChangePriorityValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");

            RuleFor(v => v.incidentId)
                .Must(d => d is string)
                .WithMessage("IncidentId must be a string!");

            RuleFor(v => v.toPriority)
                .NotEmpty()
                .WithMessage("Priority can not null!");
        }
    }
}