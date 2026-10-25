using WorkOMat.Domain.RefreshToken;
using WorkOMat.Domain.User;

namespace WorkOMat.Application.Common.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(UserId userId);
}