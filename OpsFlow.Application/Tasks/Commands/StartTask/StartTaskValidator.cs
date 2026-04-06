using FluentValidation;

namespace OpsFlow.Application.Tasks.Commands.StartTask
{
    public class StartTaskValidator : AbstractValidator<StartTaskCommand>
    {
        public StartTaskValidator()
        {
            RuleFor(v => v.taskId)
                .NotEmpty()
                .WithMessage("TaskId can not null!");

            RuleFor(v => v.taskId)
                .Must(d => d is string)
                .WithMessage("TaskId must be string type!");

            RuleFor(v => v.incidentId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");

            RuleFor(v => v.incidentId)
                .Must(d => d is string)
                .WithMessage("IncidentId must be string type!");
        }
    }
}