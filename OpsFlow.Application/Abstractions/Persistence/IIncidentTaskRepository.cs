using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentTaskRepository
    {
        Task AddAsync(IncidentTask task);
        Task UpdateAsync(IncidentTask task);
        Task<IncidentTask> GetByIdAsync(int taskId);
        Task<List<IncidentTask>> GetAllAsync();
        Task DeleteAsync(IncidentTask task);
    }
}