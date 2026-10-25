namespace WorkOMat.Domain.User;

public readonly record struct UserId(Guid Value)
{
    public static UserId New() => new(Guid.NewGuid());
    public static UserId From(Guid value) => new(value);
    public static UserId Parse(string value)
    {
        if (Guid.TryParse(value, out var guid))
        {
            return new UserId(guid);
        }
        throw new ArgumentException("Invalid UserId format.", nameof(value));
    }
    public override string ToString() => Value.ToString();
}