using FluentValidation;

namespace OpsFlow.Application.Tasks.Queries.GetTaskDetail
{
    public class GetTaskDetailValidator : AbstractValidator<GetTaskDetailQuery>
    {
        public GetTaskDetailValidator()
        {
            RuleFor(v => v.taskId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");

            RuleFor(v => v.taskId)
                .Must(d => d is string)
                .WithMessage("IncidentId must be string type!");
        }
    }
}