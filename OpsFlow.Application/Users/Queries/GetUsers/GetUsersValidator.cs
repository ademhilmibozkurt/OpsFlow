using FluentValidation;

namespace OpsFlow.Application.Users.Queries.GetUsers
{
    public class GetUsersValidator: AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
        }
    }
}