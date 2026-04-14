using FluentValidation;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Application.Incidents.Queries.GetIncidents
{
    public class GetIncidentsValidator : AbstractValidator<GetIncidentsQuery>
    {
        public GetIncidentsValidator()
        {
            RuleFor(v => v.State)
                .NotEmpty()
                .WithMessage("State can not null!");

            /* RuleFor(v => v.State)
                .Must(d => d is IncidentState)
                .WithMessage("State must be a IncidentState enum type!"); */

            RuleFor(v => v.Priority)
                .NotEmpty()
                .WithMessage("Priority can not null!");

            /* RuleFor(v => v.Priority)s
                .Must(d => d is IncidentPriority)
                .WithMessage("Priority must be IncidentPriority enum type!"); */
        }
    }
}