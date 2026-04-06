using FluentValidation;

namespace OpsFlow.Application.Tasks.Queries.GetMyTasks
{
    public class GetMyTasksValidator : AbstractValidator<GetMyTasksQuery>
    {
        public GetMyTasksValidator()
        {
        }
    }
}