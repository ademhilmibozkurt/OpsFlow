using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        string? UserId {get;}
        string? UserName {get;}
        string? Email {get;}
        string? Role {get;}
        bool IsAuthenticated {get;}
    }
}