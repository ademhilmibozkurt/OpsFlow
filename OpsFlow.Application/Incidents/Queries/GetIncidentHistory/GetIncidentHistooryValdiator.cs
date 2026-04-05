using FluentValidation;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentHistory
{
    public class GetIncidentHistoryValidator : AbstractValidator<GetIncidentHistoryQuery>
    {
        public GetIncidentHistoryValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");

            RuleFor(v => v.incidentId)
                .Must(d => d is string)
                .WithMessage("IncidentId must be a string!");
        }
    }
}