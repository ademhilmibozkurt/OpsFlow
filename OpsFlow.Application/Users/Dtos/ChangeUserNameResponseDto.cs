using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Dtos
{
    public sealed record ChangeUserNameResponseDto
    (
        string FullName,
        string UserName,
        DateTime ChangedAt
    );
}