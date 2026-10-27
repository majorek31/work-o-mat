using WorkOMat.Domain.RefreshToken;
using WorkOMat.Domain.User;

namespace WorkOMat.Application.Common.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
}