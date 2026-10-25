namespace WorkOMat.Domain.Shared;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}