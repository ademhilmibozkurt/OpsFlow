using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Persistence.Repositories
{
    public class IncidentHistorRepository : IIncidentHistoryRepository
    {
        public Task AddAsync(IncidentHistory history)
        {
            throw new NotImplementedException();
        }
    }
}