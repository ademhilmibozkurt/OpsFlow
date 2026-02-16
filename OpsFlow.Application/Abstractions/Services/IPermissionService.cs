using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IPermissionService
    {
        // incidents
        void CanCreateIncident(User user);
        void CanChangePriority(int createdById, User user);
        void CanCloseIncident(User user);
        void CanAbortIncident(User user);
        void CanInvestigateIncident(User user);
        void CanDeleteIncident(User user);
    
        // tasks
        void CanCreateTask(User user);
        void CanAssignTask(User user);
        void CanStartTask(User user, int assignedId);
        void CanCloseTask(User user, int assignedId);
        void CanAbortTask(User user);
    }
}