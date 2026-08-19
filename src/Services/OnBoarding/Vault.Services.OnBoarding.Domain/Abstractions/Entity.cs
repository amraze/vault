namespace Vault.Services.OnBoarding.Domain.Abstractions
{
    /// <summary>Base class for domain entities : (Entity Id) </summary>
    public abstract class Entity<TId> where TId : struct
    {
        public TId Id { get; }

        protected Entity(TId id) => Id = id;

        protected Entity() { }
    }
}