using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Common.Exceptions;
using OpsFlow.Domain.Entities;
using OpsFlow.Infrastructure.Persistence.AppContext;

namespace OpsFlow.Infrastructure.Persistence.Repositories
{
    public class IncidentHistoryRepository : IIncidentHistoryRepository
    {
        private readonly AppDbContext _context;

        public IncidentHistoryRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task AddAsync(IncidentHistory history, CancellationToken cancellationToken)
        {
            await _context.Histories.AddAsync(history);
        }

        public async Task<IncidentHistory> GetByIdAsync(string incidentId, CancellationToken cancellationToken)
        {
            return await _context.Histories.FindAsync(incidentId, cancellationToken) ?? throw new NotFoundException("Incident history not found!");
        }

        public async Task<IQueryable<IncidentHistory>> Query(CancellationToken cancellationToken)
        {
            return _context.Histories.AsNoTracking();
        }
    }
}