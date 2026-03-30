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
        void CanGetIncidentDetail(string createdById, string userId, string userRole);
        void CanGetIncidentHistory(string createdById, string userId, string userRole);
    
        // tasks
        void CanCreateTask(string userRole);
        void CanAssignTask(string userRole);
        void CanStartTask(string userId, string assignedId);
        void CanCloseTask(string userId, string assignedId);
        void CanAbortTask(string userRole);
        void CanDeleteTask(string userRole);
        void CanGetTaskDetail(string createdById, string userId, string userRole);
        void CanGetIncidentTasks(string createdById, string userId, string userRole);
        void CanGetTaskHistory(string createdById, string userId, string userRole);

        // users
        void CanDeleteUser(string userId, string currentUserId, string userRole);
        void CanChangeRole(string userRole);
        void CanChangePassword(string currentUserId, string userId);
        void CanUpdateProfile(string currentUserId, string userId, string Role);
        void CanChangeUserName(string currentUserId, string userId);
        void CanGetUserDetail(string userRole);
        void CanGetUsers(string userRole);
    }
}