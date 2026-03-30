using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetUserActivity
{
    public sealed record GetUserActivityQuery
    (
        string userId,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<UserActivityItemDto>>;
}