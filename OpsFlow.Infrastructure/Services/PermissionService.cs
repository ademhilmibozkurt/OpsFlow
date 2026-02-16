using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        // incidents
        public void CanCreateIncident(User user)
        {
            // every role can create incident
            // method should stay for future changes
            return;
        }

        public void CanChangePriority(int createdById, User user)
        {
            if (createdById == user.Id)
            {
                return;
            }
            else if (user.Role == Roles.User)
            {
                throw new ForbiddenException($"{user.Role} can not change incident priority!");
            }
        }

        public void CanCloseIncident(User user)
        {
            if (user.Role == Roles.User)
            {
                throw new ForbiddenException("User not allow to close incidents!");
            }
        }

        public void CanAbortIncident(User user)
        {
            if (user.Role != Roles.Admin)
            {
                throw new ForbiddenException($"{user.Role} not allow to abort incidents!");
            }
        }

        public void CanInvestigateIncident(User user)
        {
            if (user.Role != Roles.IncidentManager)
            {
                throw new ForbiddenException($"{user.Role} not allow to investigate incidents!");
            }
        }

        public void CanDeleteIncident(User user)
        {
            if (user.Role == Roles.User)
            {
                throw new ForbiddenException($"{user.Role} not allow to delete incidents!");
            }
        }


        // tasks
        public void CanCreateTask(User user)
        {
            if (user.Role == Roles.User)
            {
                throw new ForbiddenException($"{user.Role} can not create task!");
            }
        }

        public void CanAssignTask(User user)
        {
            if (user.Role == Roles.User)
            {
                throw new ForbiddenException($"{user.Role} can not assign task to someone!");
            }
        }

        public void CanStartTask(User user, int assignedId)
        {
            if (user.Id != assignedId)
            {
                throw new ForbiddenException($"User {user.Id} can not start task. User not assigned the task!");
            }
        }

        public void CanCloseTask(User user, int assignedId)
        {
            if (user.Id != assignedId)
            {
                throw new ForbiddenException($"User {user.Id} can not close task. User not assigned the task!");
            }
        }

        public void CanAbortTask(User user)
        {
            if (user.Role == Roles.User)
            {
                throw new ForbiddenException("User can not abort task!");
            }
        }

        public void CanDeleteTask(User user)
        {
            if (user.Role == Roles.User)
            {
                throw new ForbiddenException("User can not delete task!");
            }
        }

    }
}