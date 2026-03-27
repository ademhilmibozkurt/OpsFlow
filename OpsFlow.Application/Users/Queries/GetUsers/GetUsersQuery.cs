using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetUsers
{
    public sealed record GetUsersQuery
    (
        string userId,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<PaginatedResponseDto<UserItemDto>>;
}