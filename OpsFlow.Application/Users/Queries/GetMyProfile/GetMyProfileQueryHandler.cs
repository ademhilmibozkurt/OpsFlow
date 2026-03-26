using System.Security.Authentication;
using MediatR;
using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Application.Identity;
using OpsFlow.Application.Users.Dtos;

namespace OpsFlow.Application.Users.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, GetUserDetailResponseDto>
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUser;

        public GetMyProfileQueryHandler
        (
            IUserService userService,
            ICurrentUserService currentUser
        )
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        public async Task<GetUserDetailResponseDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            // getCurrentUser
            string userId = _currentUser.UserId ?? throw new AuthenticationException("User not authenticated!");

            // getUserDetail
            AppUser user = await _userService.FindByIdAsync
            (
                userId
            ) ?? throw new NotFoundException("User not found!");

            // returnDto
            return new GetUserDetailResponseDto
            (
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.Role,
                user.CreatedAt
            );
        }
    }
}