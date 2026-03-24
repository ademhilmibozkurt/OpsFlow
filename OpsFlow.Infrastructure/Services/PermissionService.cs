using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;

namespace OpsFlow.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        // incidents
        public void CanCreateIncident(string userRole)
        {
            // every role can create incident
            // method should stay for future changes
            return;
        }

        public void CanChangePriority(string userRole, string userId, string createdById)
        {
            if (createdById == userId)
            {
                return;
            }
            else if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} can not change incident priority!");
            }
        }

        public void CanCloseIncident(string userRole)
        {
            if (userRole == "User")
            {
                throw new ForbiddenException("User not allow to close incidents!");
            }
        }

        public void CanAbortIncident(string userRole)
        {
            if (userRole != "Admin")
            {
                throw new ForbiddenException($"{userRole} not allow to abort incidents!");
            }
        }

        public void CanInvestigateIncident(string userRole)
        {
            if (userRole != "IncidentManager")
            {
                throw new ForbiddenException($"{userRole} not allow to investigate incidents!");
            }
        }

        public void CanDeleteIncident(string userRole)
        {
            if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} not allow to delete incidents!");
            }
        }

        public void CanGetIncidentDetail(string createdById, string userId, string userRole)
        {
            if (createdById == userId)
            {
                return;
            }
            else if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} can not get incident detail!");
            }
        }

        public void CanGetIncidentHistory(string createdById, string userId, string userRole)
        {
            if (createdById == userId)
            {
                return;
            }
            else if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} can not get incident detail!");
            }
        }


        // tasks
        public void CanCreateTask(string userRole)
        {
            if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} can not create task!");
            }
        }

        public void CanAssignTask(string userRole)
        {
            if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} can not assign task to someone!");
            }
        }

        public void CanStartTask(string userId, string assignedId)
        {
            if (userId != assignedId)
            {
                throw new ForbiddenException($"User {userId} can not start task. User not assigned the task!");
            }
        }

        public void CanCloseTask(string userId, string assignedId)
        {
            if (userId != assignedId)
            {
                throw new ForbiddenException($"User {userId} can not close task. User not assigned the task!");
            }
        }

        public void CanAbortTask(string userRole)
        {
            if (userRole == "User")
            {
                throw new ForbiddenException("User can not abort task!");
            }
        }

        public void CanDeleteTask(string userRole)
        {
            if (userRole == "User")
            {
                throw new ForbiddenException("User can not delete task!");
            }
        }

        public void CanGetTaskDetail(string createdById, string userId, string userRole)
        {
            if (createdById == userId)
            {
                return;
            }
            else if (userRole == "User")
            {
                throw new ForbiddenException($"{userRole} can not get task detail!");
            }
        }

        
        // Users
        public void CanDeleteUser(string userId, string currentUserId, string userRole)
        {
            if (userId == currentUserId)
            {
                return ;
            }

            if (userRole != "Admin")
            {
                throw new ForbiddenException("User can not delete user!");
            }
        }

        public void CanChangeRole(string userRole)
        {
            if (userRole != "Admin")
            {
                throw new ForbiddenException($"{userRole} can not change role!");
            }
        }

        public void CanChangePassword(string currentUserId, string userId)
        {
            if(currentUserId != userId)
            {
                throw new ForbiddenException("Only account owner can change password!");
            }
        }

        public void CanUpdateProfile(string currentUserId, string userId, string userRole)
        {
            if (userId == currentUserId)
            {
                return ;
            }

            if (userRole != "Admin")
            {
                throw new ForbiddenException($"{userRole} can not update profile!");
            }
        }

        public void CanChangeUserName(string currentUserId, string userId)
        {
            if (currentUserId != userId)
            {
                throw new ForbiddenException("Only account owner change user name!");
            }
        }
    }
}