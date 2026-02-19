using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Services
{
    public interface IPermissionService
    {
        // incidents
        void CanCreateIncident(string userRole);
        void CanChangePriority(string userRole, string userId, string createdById);
        void CanCloseIncident(string userRole);
        void CanAbortIncident(string userRole);
        void CanInvestigateIncident(string userRole);
        void CanDeleteIncident(string userRole);
    
        // tasks
        void CanCreateTask(string userRole);
        void CanAssignTask(string userRole);
        void CanStartTask(string userId, string assignedId);
        void CanCloseTask(string userId, string assignedId);
        void CanAbortTask(string userRole);
        void CanDeleteTask(string userRole);
    }
}