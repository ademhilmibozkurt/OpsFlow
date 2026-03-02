using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Dtos
{
    public sealed record ChangeRoleResponseDto
    (
        string FullName,
        string UserName,
        AppRole Role
    );
}