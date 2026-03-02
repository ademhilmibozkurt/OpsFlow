using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Dtos
{
    public sealed record GetUserDetailResponseDto
    (
        string FullName,
        string UserName,
        string Email,
        string PhoneNumber,
        AppRole Role,
        DateTime CreatedAt
    );
}