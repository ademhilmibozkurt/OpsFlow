using FluentValidation;

namespace OpsFlow.Application.Tasks.Commands.AssignTask
{
    public class AssignTaskValidator : AbstractValidator<AssignTaskCommand>
    {
        public AssignTaskValidator()
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
            
            RuleFor(v => v.assigneeId)
                .NotEmpty()
                .WithMessage("AssigneeId can not null!");

            RuleFor(v => v.assigneeId)
                .Must(d => d is string)
                .WithMessage("AssigneeId must be string type!");
        }
    }
}