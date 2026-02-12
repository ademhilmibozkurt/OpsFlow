using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Domain.Entities;
using OpsFlow.Infrastructure.Persistence.DbContext;

namespace OpsFlow.Infrastructure.Persistence.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        // dependency injection
        private readonly AppDbContext _context;

        public IncidentRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task AddAsync(Incident incident)
        {
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
        }
    
        public async Task UpdateAsync(Incident incident)
        {
            _context.Entry(incident).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public async Incident GetByIdAsync(int incidentId)
        {
            return _context.Incidents.FindAsync(incidentId);
        }

        public async Incident GetWithTaskId(int taskId)
        {
            return _context.Incidents.FindAsync(taskId);
        }

        public async List<Task> GetTasksAsync(int incidentId)
        {
            return _context.Incidents.FindAsync(incidentId);
        }

        public async List<User> GetUsersAsync(int incidentId)
        {
            return _context.Incidents.FindAsync(incidentId);
        }

        public async Task DeleteAsync(int incidentId)
        {
            Incident incident = _context.Incidents.FindAsync(incidentId);
            _context.DeleteAsync(incident);
            _context.SaveChanges();
        }
    }
}