using WorkOMat.Domain.Shared;
using WorkOMat.Domain.User;

namespace WorkOMat.Domain.RefreshToken;

public class RefreshToken : AggregateRoot<RefreshTokenId>
{
    public string Token { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public UserId UserId { get; private set; }

    public RefreshToken(RefreshTokenId id, string token, DateTime expiresAt, UserId userId) : base(id)
    {
        Token = token;
        ExpiresAt = expiresAt;
        UserId = userId;
    }

    public void UpdateToken(string newToken, DateTime newExpiresAt)
    {
        Token = newToken;
        ExpiresAt = newExpiresAt;
    }

}