using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Domain.Entities;
using OpsFlow.Infrastructure.Persistence.AppContext;

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
            await _context.Incidents.AddAsync(incident);
        }
    
        public async Task UpdateAsync(Incident incident)
        {
            _context.Incidents.Update(incident);
        }

        public async Task<Incident> GetByIdAsync(int incidentId)
        {
            return await _context.Incidents.FindAsync(incidentId);
        }

        public async Task<List<Incident>> GetAllAsync()
        {
            return await _context.Incidents.ToListAsync();
        }

        public async Task DeleteAsync(Incident incident)
        {
            _context.Incidents.Remove(incident);
        }
    }
}