using MediatR;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetMyProfile
{
    public sealed record GetMyProfileQuery() : IRequest<GetUserDetailResponseDto>;
}