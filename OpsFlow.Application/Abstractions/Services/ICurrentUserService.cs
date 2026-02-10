using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        public User Get();
    }
}