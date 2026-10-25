namespace WorkOMat.Domain.RefreshToken;

public readonly record struct RefreshTokenId(Guid Value)
{
    public static RefreshTokenId New() => new(Guid.NewGuid());
    public static RefreshTokenId From(Guid value) => new(value);
    public static RefreshTokenId Parse(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new RefreshTokenId(guid);
        }
        throw new ArgumentException("Invalid RefreshTokenId format.", nameof(value));
    }
    public override string ToString() => Value.ToString();

}