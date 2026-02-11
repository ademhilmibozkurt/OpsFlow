using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentRepository
    {
        Task AddAsync(Incident incident);
    }
}