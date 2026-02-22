using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Dtos
{
    public sealed record ChangeRoleResponseDto
    (
        Guid UserId,
        string FullName,
        string UserName,
        AppRole Role
    );
}