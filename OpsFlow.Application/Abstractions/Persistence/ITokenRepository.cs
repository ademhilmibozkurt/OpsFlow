using OpsFlow.Application.Common.Results;
using OpsFlow.Application.Models;

namespace OpsFlow.Application.Abstractions.Persistence
{
    public interface ITokenRepository
    {
        Task AddAsync(string refreshToken, string userId, DateTime expiresAt, CancellationToken cancellationToken);
        Task<Result<RefreshTokenModel>> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task RevokeAsync(string refreshToken, CancellationToken cancellationToken);
        Task RevokeAllAsync(string userId, CancellationToken cancellationToken);
    }
}