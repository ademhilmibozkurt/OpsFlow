using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetUserDetail
{
    public sealed record GetUserDetailQuery(string userId) : IRequest<GetUserDetailResponseDto>;
}