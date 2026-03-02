namespace OpsFlow.Application.Users.Dtos
{
    public record ChangePasswordResponseDto
    (
        string UserName,
        DateTime OccuredAt
    );
}