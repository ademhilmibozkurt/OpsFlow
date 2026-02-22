namespace OpsFlow.Application.Users.Commands.Register
{
    public record RegisterCommand(string fullName, string userName, string email, string phoneNumber, string password);
}