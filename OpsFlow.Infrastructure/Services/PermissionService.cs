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
                throw new ForbiddenException("User not allow the close incidents!");
            }
        }
    }
}