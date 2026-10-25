namespace WorkOMat.Domain.ValueObjects.Auth;

public sealed record RefreshToken(string Value, DateTime Expiry)
{
    public static RefreshToken Create(string value, DateTime expiry)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Refresh token cannot be null or empty.", nameof(value));
        }

        if (expiry <= DateTime.UtcNow)
        {
            throw new ArgumentException("Refresh token expiry must be in the future.", nameof(expiry));
        }

        return new RefreshToken(value, expiry);
    }

    public override string ToString() => Value;

}