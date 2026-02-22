namespace OpsFlow.Application.Users.Dtos
{
    public sealed record UpdateProfileResponseDto
    (
        Guid UserId,
        string FullName,
        string UserName,
        string Email,
        string PhoneNumber
    );
}