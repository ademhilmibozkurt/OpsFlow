using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IPermissionService
    {
        public bool CanCreateIncident(User user);
    }
}