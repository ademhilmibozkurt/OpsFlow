using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Users.Dtos
{
    public sealed record UserItemDto
    (
        string UserId,
        string FullName,
        string UserName,
        string Email,
        string PhoneNumber,
        string Role,
        DateTime CreatedAt
    );
}