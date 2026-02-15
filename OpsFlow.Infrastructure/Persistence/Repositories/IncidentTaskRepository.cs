using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Domain.Entities;
using OpsFlow.Infrastructure.Persistence.AppContext;

namespace OpsFlow.Infrastructure.Persistence.Repositories
{
    public class IncidentTaskRepository : IIncidentTaskRepository
    {
        private readonly AppDbContext _context;
        public IncidentTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(IncidentTask task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task DeleteAsync(IncidentTask task)
        {
            _context.Remove(task);
        }

        public async Task<List<IncidentTask>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<IncidentTask> GetByIdAsync(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task UpdateAsync(IncidentTask task)
        {
            _context.Tasks.Update(task);
        }
    }
}