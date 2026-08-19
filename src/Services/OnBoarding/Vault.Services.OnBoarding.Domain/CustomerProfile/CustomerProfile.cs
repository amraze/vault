using Vault.Services.OnBoarding.Domain.Abstractions;

namespace Vault.Services.OnBoarding.Domain.CustomerProfile
{
    /// <summary>Aggregate Root for customer profiles aggregate.</summary>
    public class CustomerProfile : AggregateRoot<Guid>
    {
        public required string UserId { get; set; }
    }
}
