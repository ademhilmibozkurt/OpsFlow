using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Persistence
{
    public class IncidentRepository : IIncidentRepository
    {
        public Task AddAsync(Incident incident)
        {
            throw new NotImplementedException();
        }
    }
}