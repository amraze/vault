namespace Vault.Services.OnBoarding.Domain.Abstractions
{
    /// <summary>Base class for aggregate roots : Domain Events, RaiseEvent() and ClearDomainEvents().</summary>
    public abstract class AggregateRoot<TId> : Entity<TId>, IDomainEventCollection where TId : struct
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot(TId id) : base(id) { }

        protected AggregateRoot() { }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected void RaiseEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}