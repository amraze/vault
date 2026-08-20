namespace Vault.Services.OnBoarding.Domain.Abstractions
{
    /// <summary>Base class for domain entities : (Entity Id). Equality is by identity, not by value.</summary>
    public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : struct
    {
        protected Entity() { } // EF
        protected Entity(TId id) => Id = id;

        public TId Id { get; }

        public bool Equals(Entity<TId>? other) => other is not null && other.GetType() == GetType() && other.Id.Equals(Id);
        public override bool Equals(object? obj) => Equals(obj as Entity<TId>);
        public override int GetHashCode() => HashCode.Combine(GetType(), Id);
    }
}
