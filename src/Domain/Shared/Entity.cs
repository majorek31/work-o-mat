namespace WorkOMat.Domain.Shared;

public abstract class Entity<TId> : IEntity
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }
}