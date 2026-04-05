using FluentValidation;

namespace OpsFlow.Application.Incidents.Queries.GetIncidentDetail
{
    public class GetIncidentDetailValidator : AbstractValidator<GetIncidentDetailQuery>
    {
        public GetIncidentDetailValidator()
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