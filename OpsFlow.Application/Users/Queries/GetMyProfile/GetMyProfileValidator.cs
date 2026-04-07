using FluentValidation;

namespace OpsFlow.Application.Users.Queries.GetMyProfile
{
    public class GetMyProfileValidator: AbstractValidator<GetMyProfileQuery>
    {
        public GetMyProfileValidator()
        {
        }
    }
}