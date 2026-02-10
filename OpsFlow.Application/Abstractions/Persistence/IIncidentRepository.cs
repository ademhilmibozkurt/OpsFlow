using OpsFlow.Domain.Entities;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IIncidentRepository
    {
        public void AddAsync(Incident incident);
    }
}