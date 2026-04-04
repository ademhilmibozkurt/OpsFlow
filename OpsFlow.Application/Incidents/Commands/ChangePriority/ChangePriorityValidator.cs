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
                .Must(d => d is string)
                .WithMessage("IncidentId can not null. Must be string type!");

            RuleFor(v => v.toPriority)
                .NotEmpty()
                .Must(d => d is IncidentPriority)
                .WithMessage("Priority can not null and must be IncidentPriority type!");
        }
    }
}