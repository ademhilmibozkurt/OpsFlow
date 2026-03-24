using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentHistoryRepository
    {
        Task AddAsync(IncidentHistory history, CancellationToken cancellationToken);
        Task<IncidentHistory> GetByIdAsync(string incidentId, CancellationToken cancellationToken);
        Task<IQueryable<IncidentHistory>> Query(CancellationToken cancellationToken);
    }
}