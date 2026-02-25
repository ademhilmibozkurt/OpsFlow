using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident);
        Task UpdateAsync(Incident incident);
        Task<Incident> GetByIdAsync(string incidentId);
        Task<List<Incident>> GetAllAsync();
        Task DeleteAsync(Incident incident);
    }
}