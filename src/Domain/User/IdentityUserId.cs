namespace WorkOMat.Domain.User;

public readonly record struct IdentityUserId(Guid Value)
{
    public static IdentityUserId New() => new(Guid.NewGuid());
    public static IdentityUserId From(Guid value) => new(value);
    public static IdentityUserId Parse(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new IdentityUserId(guid);
        }
        throw new ArgumentException("Invalid IdentityUserId format.", nameof(value));
    }
    public override string ToString() => Value.ToString();
}