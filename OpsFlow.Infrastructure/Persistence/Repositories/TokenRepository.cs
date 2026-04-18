using Microsoft.EntityFrameworkCore;
using OpsFlow.Application.Abstractions.Persistence;
using OpsFlow.Application.Common.Results;
using OpsFlow.Application.Models;
using OpsFlow.Infrastructure.Entities;
using OpsFlow.Infrastructure.Persistence.AppContext;

namespace OpsFlow.Infrastructure.Persistence.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        // dependency injection
        private readonly AppDbContext _context;

        public TokenRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task AddAsync(string refreshToken, string userId, DateTime expiresAt, CancellationToken cancellationToken)
        {
            await _context.Tokens.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                ExpiresAt = expiresAt,
                IsRevoked = false
            }, 
            cancellationToken);
        }

        public async Task<Result<RefreshTokenModel>> GetByTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            RefreshToken? model = await _context.Tokens.FirstOrDefaultAsync
            (
                x => x.Token == refreshToken, cancellationToken
            );

            if (model == null)
                return Result<RefreshTokenModel>.Failure("Model is null!");

            return Result<RefreshTokenModel>.Success(new RefreshTokenModel
            {
                Token = model.Token,
                UserId = model.UserId,
                ExpiresAt = model.ExpiresAt,
                IsRevoked = model.IsRevoked
            });
        }

        public async Task RevokeAsync(string refreshToken, CancellationToken cancellationToken)
        {
            RefreshToken? model = await _context.Tokens.FirstOrDefaultAsync
            (
                x => x.Token == refreshToken, cancellationToken
            );
            
            if (model != null)
                model.IsRevoked = true;

            await _context.SaveChangesAsync();
        }

        public async Task RevokeAllAsync(string userId, CancellationToken cancellationToken)
        {
            List<RefreshToken> models = _context.Tokens.Where(x => x.UserId == userId && !x.IsRevoked).ToList();
            foreach(RefreshToken model in models)
            {
                model.IsRevoked = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}