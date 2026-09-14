namespace Vault.Services.OnBoarding.Domain.Abstractions
{
    /// <summary>Base class for domain models : (Entity Id). Equality is by identity, not by value.</summary>
    public abstract class DomainModel<TId> : IEquatable<DomainModel<TId>> where TId : struct
    {
        protected DomainModel() { } // EF
        protected DomainModel(TId id) => Id = id;

        public TId Id { get; }

        public bool Equals(DomainModel<TId>? other) => other is not null && other.GetType() == GetType() && other.Id.Equals(Id);
        public override bool Equals(object? obj) => Equals(obj as DomainModel<TId>);
        public override int GetHashCode() => HashCode.Combine(GetType(), Id);
    }
}
