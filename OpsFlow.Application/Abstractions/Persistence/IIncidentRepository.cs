using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident);
        Task UpdateAsync(Incident incident);
        Incident GetByIdAsync(int incidentId);
        Incident GetWithTaskId(int taskId);
        List<Task> GetTasksAsync(int incidentId);
        List<User> GetUsersAsync(int incidentId);
        Task DeleteAsync(int incidentId);
    }
}