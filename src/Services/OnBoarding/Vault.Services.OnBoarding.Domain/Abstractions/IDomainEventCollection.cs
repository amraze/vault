namespace Vault.Services.OnBoarding.Domain.Abstractions
{
    /// <summary>Ports for aggregate roots that can raise domain events.</summary>
    public interface IDomainEventCollection
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }

    public interface IDomainEvent
    {
        DateTimeOffset OccurredOn { get; }
    }
}
