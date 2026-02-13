using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident);
        Task UpdateAsync(Incident incident);
        Task<Incident> GetByIdAsync(int incidentId);
        Task<List<Incident>> GetAllAsync();
        Task DeleteAsync(Incident incident);
    }
}