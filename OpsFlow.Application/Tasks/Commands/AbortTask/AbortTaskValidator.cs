using FluentValidation;

namespace OpsFlow.Application.Tasks.Commands.AbortTask
{
    public class AbortTaskValidator : AbstractValidator<AbortTaskCommand>
    {
        public AbortTaskValidator()
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
            
            RuleFor(v => v.abortionNote)
                .NotEmpty()
                .WithMessage("Abortion Note can not null!");

            RuleFor(v => v.abortionNote)
                .Matches("(\b\\w+\b.*){3}")
                .WithMessage("Abortion Note must at least three words!");
        }
    }
}