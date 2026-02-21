using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Infrastructure.Identity;
using OpsFlow.Infrastructure.Persistence.AppContext;

namespace OpsFlow.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AppUser user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(AppUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(AppUser user)
        {
            _context.Users.Update(user);
        }
    }
}