namespace WorkOMat.Domain.Shared;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    public readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected AggregateRoot(TId id) : base(id)
    {
    }
}