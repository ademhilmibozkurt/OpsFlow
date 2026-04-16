using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Common.Exceptions;
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

        public async Task AddAsync(Incident incident, CancellationToken cancellationToken)
        {
            await _context.Incidents.AddAsync(incident, cancellationToken);
        }
    
        public async Task UpdateAsync(Incident incident, CancellationToken cancellationToken)
        {
            await Task.FromResult(_context.Incidents.Update(incident));
        }

        public async Task<Incident> GetByIdAsync(string incidentId, CancellationToken cancellationToken)
        {
            return await _context.Incidents.FindAsync(incidentId, cancellationToken) ?? throw new NotFoundException("Incident not found!");
        }

        public async Task<List<Incident>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Incidents.ToListAsync(cancellationToken);
        }

        public async Task DeleteAsync(Incident incident, CancellationToken cancellationToken)
        {
            await Task.FromResult(_context.Incidents.Remove(incident));
        }

        public IQueryable<Incident> Query(CancellationToken cancellationToken)
        {
            return _context.Incidents.AsNoTracking();
        }

        public IQueryable<IncidentTask> TaskQuery(CancellationToken cancellationToken)
        {
            return _context.Tasks.AsNoTracking();
        }
    }
}