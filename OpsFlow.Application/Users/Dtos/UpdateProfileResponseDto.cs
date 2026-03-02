namespace OpsFlow.Application.Users.Dtos
{
    public sealed record UpdateProfileResponseDto
    (
        string FullName,
        string UserName,
        string Email,
        string PhoneNumber,
        DateTime OccuredAt
    );
}