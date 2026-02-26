using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident, CancellationToken cancellationToken);
        Task UpdateAsync(Incident incident, CancellationToken cancellationToken);
        Task<Incident> GetByIdAsync(string incidentId, CancellationToken cancellationToken);
        Task<List<Incident>> GetAllAsync(CancellationToken cancellationToken);
        Task DeleteAsync(Incident incident, CancellationToken cancellationToken);
    }
}