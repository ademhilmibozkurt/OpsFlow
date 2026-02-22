using OpsFlow.Application.Identity;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        public AppUser Get();
    }
}