using Microsoft.EntityFrameworkCore;
using WorkOMat.Application.Common.Repositories;
using WorkOMat.Domain.RefreshToken;
using WorkOMat.Domain.User;

namespace WorkOMat.Infrastructure.Persistance.Repositories;

public class RefreshTokenRepository : Repository<RefreshToken, RefreshTokenId>, IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token && rt.ExpiresAt > DateTime.UtcNow, cancellationToken);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens.Where(rt => rt.UserId == userId && rt.ExpiresAt > DateTime.UtcNow).ToListAsync();
    }
}