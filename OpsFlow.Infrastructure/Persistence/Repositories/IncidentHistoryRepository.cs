using OpsFlow.Application.Abstractions.Persistence;
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

        public async Task AddAsync(IncidentHistory history)
        {
            await _context.Histories.AddAsync(history);
        }
    }
}