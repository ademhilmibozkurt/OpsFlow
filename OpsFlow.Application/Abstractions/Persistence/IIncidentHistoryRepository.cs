using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentHistoryRepository
    {
        public void AddAsync(IncidentHistory history);
    }
}