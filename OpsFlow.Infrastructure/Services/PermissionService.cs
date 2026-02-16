using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Domain.Entities;
using OpsFlow.Domain.Enums;

namespace OpsFlow.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
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
    }
}