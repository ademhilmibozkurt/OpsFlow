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

        // GetById

        // GetWithTasks

        // ListByUser

        // Remove
    }
}