using FluentValidation;

namespace OpsFlow.Application.Users.Queries.GetUserActivity
{
    public class GetUserActivityValidator: AbstractValidator<GetUserActivityQuery>
    {
        public GetUserActivityValidator()
        {
            RuleFor(v => v.userId)
                .NotEmpty()
                .WithMessage("UserId can not null!");

            RuleFor(v => v.userId)
                .Must(d => d is string)
                .WithMessage("UserId must be string type!");

            RuleFor(v => v.fromDate)
                .NotEmpty()
                .WithMessage("FromDate can not null!");

            RuleFor(v => v.fromDate)
                .Must(d => d is DateTime)
                .WithMessage("FromDate must be DateTime type!");   

            RuleFor(v => v.toDate)
                .NotEmpty()
                .WithMessage("ToDate can not null!");

            RuleFor(v => v.toDate)
                .Must(d => d is DateTime)
                .WithMessage("ToDate must be DateTime type!");  

            RuleFor(v => v.onlyTasks)
                .NotEmpty()
                .WithMessage("OnlyTasks can not null!");

            RuleFor(v => v.onlyTasks)
                .Must(d => d is bool)
                .WithMessage("OnlyTasks must be boolean!");
        }
    }
}