using FluentValidation;

namespace OpsFlow.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(v => v.incidentId)
                .NotEmpty()
                .WithMessage("IncidentId can not null!");

            RuleFor(v => v.incidentId)
                .Must(d => d is string)
                .WithMessage("IncidentId must be string type!");

            RuleFor(v => v.title)
                .NotEmpty()
                .WithMessage("Title can not null!");

            RuleFor(v => v.title)
                .Must(d => d is string)
                .WithMessage("Title must be string type!");

            RuleFor(v => v.title)
                .Matches("(\b\\w+\b.*){2}")
                .WithMessage("Title must at least two words!");

            RuleFor(v => v.note)
                .NotEmpty()
                .WithMessage("Description can not null!");

            RuleFor(v => v.note)
                .Must(d => d is string)
                .WithMessage("Note must be string!");
            
            RuleFor(v => v.note)
                .Matches("(\b\\w+\b.*){3}")
                .WithMessage("Note must at least three words!");
        }
    }
}