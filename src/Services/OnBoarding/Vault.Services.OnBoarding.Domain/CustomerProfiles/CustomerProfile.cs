using Vault.Services.OnBoarding.Domain.Abstractions;
using Vault.Services.OnBoarding.Domain.CustomerProfiles.Enums;

namespace Vault.Services.OnBoarding.Domain.CustomerProfiles
{
    /// <summary>Aggregate Root for customer profiles aggregate.</summary>
    public sealed class CustomerProfile : AggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public KycState KycStatus { get; set; }
        public DateTime? VerifiedAt { get; set; }
    }
}
