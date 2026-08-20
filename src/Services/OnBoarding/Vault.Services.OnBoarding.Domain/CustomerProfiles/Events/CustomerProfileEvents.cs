using Vault.Services.OnBoarding.Domain.Abstractions;

namespace Vault.Services.OnBoarding.Domain.CustomerProfiles.Events
{
    /// <summary>A customer profile was registered and awaits KYC.</summary>
    public sealed record CustomerProfileRegistered(Guid CustomerProfileId, Guid UserId, DateTimeOffset OccurredOn) : IDomainEvent;

    /// <summary>A customer profile was submitted for KYC review.</summary>
    public sealed record KycSubmitted(Guid CustomerProfileId, DateTimeOffset OccurredOn) : IDomainEvent;

    /// <summary>A customer profile passed KYC verification.</summary>
    public sealed record KycVerified(Guid CustomerProfileId, DateTimeOffset OccurredOn) : IDomainEvent;

    /// <summary>A customer profile failed KYC verification.</summary>
    public sealed record KycRejected(Guid CustomerProfileId, DateTimeOffset OccurredOn) : IDomainEvent;
}
