namespace OpsFlow.Application.Users.Commands.Register
{
    public record RegisterCommand(string userName, string email, string phoneNumber);
}