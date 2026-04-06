using FluentValidation;

namespace OpsFlow.Application.Tasks.Queries.GetTasksByIncident
{
    public class GetTasksByIncidentValidator : AbstractValidator<GetTasksByIncidentQuery>
    {
        public GetTasksByIncidentValidator()
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