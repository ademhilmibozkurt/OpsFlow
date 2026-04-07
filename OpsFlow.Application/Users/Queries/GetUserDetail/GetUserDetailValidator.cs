using FluentValidation;

namespace OpsFlow.Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailValidator: AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailValidator()
        {
            RuleFor(v => v.userId)
                .NotEmpty()
                .WithMessage("UserId can not null!");

            RuleFor(v => v.userId)
                .Must(d => d is string)
                .WithMessage("UserId must be string type!");
        }
    }
}