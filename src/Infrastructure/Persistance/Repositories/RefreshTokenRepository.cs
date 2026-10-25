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

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(UserId userId)
    {
        return await _context.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
    }
}