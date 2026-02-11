using OpsFlow.Application.Abstractions.Services;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Services
{
    public class PermissionService : IPermissionService
    {
        public bool CanCreateIncident(User user)
        {
            throw new NotImplementedException();
        }
    }
}