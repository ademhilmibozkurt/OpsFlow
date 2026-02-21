using OpsFlow.Infrastructure.Identity;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface IUserRepository
    {
        Task AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task<AppUser> GetByIdAsync(Guid userId);
        Task<List<AppUser>> GetAllAsync();
        Task DeleteAsync(AppUser user);
    }
}