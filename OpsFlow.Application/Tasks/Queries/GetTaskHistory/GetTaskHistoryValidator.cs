using FluentValidation;

namespace OpsFlow.Application.Tasks.Queries.GetTaskHistory
{
    public class GetTaskHistoryValidator : AbstractValidator<GetTaskHistoryQuery>
    {
        public GetTaskHistoryValidator()
        {
            RuleFor(v => v.taskId)
                .NotEmpty()
                .WithMessage("TaskId can not null!");

            RuleFor(v => v.taskId)
                .Must(d => d is string)
                .WithMessage("TaskId must be string type!");
        }
    }
}