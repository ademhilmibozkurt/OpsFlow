using Microsoft.AspNetCore.Identity;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        public IdentityUser Get();
    }
}