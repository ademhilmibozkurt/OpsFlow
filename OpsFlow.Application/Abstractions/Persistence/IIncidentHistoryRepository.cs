using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentHistoryRepository
    {
        Task AddAsync(IncidentHistory history);
    }
}