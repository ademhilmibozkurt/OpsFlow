using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IPermissionService
    {
        void CanCreateIncident(User user);
        void CanChangePriority(int createdById, User user);
        void CanCloseIncident(User user);
        void CanAbortIncident(User user);
    }
}