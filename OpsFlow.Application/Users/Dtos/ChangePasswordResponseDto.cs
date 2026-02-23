namespace OpsFlow.Application.Users.Dtos
{
    public record ChangePasswordResponseDto
    (
        string UserId,
        string UserName,
        DateTime OccuredAt
    );
}