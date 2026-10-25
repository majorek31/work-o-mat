namespace WorkOMat.Domain.ValueObjects.Auth;

public sealed record AccessToken(string Value)
{
    public static AccessToken Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Access token cannot be null or empty.", nameof(value));
        }

        return new AccessToken(value);
    }

    public override string ToString() => Value;
}