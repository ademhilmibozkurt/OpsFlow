using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

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

    }
}