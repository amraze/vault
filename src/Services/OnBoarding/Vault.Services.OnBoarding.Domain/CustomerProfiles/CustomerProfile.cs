using Vault.Services.OnBoarding.Domain.Abstractions;
using Vault.Services.OnBoarding.Domain.CustomerProfiles.Enums;

namespace Vault.Services.OnBoarding.Domain.CustomerProfiles
{
    /// <summary>Aggregate Root for customer profiles aggregate : Register(), MarkPending(), MarkVerified(), MarkRejected()</summary>
    public sealed class CustomerProfile : AggregateRoot<Guid>
    {
        private CustomerProfile() { }   // EF
        private CustomerProfile(Guid id, Guid userId, string firstName, string lastName, DateOnly dateOfBirth) : base(id)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            KycStatus = KycState.Unverified;
        }

        public Guid UserId { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public DateOnly DateOfBirth { get; private set; }
        public KycState KycStatus { get; private set; }
        public DateTime? VerifiedAt { get; private set; }

        /// <summary>Registers a new, unverified customer profile.</summary>
        public static CustomerProfile Register(Guid userId, string firstName, string lastName, DateOnly dateOfBirth, DateTime utcNow, int minimumAge)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId is required.", nameof(userId));
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentOutOfRangeException.ThrowIfNegative(minimumAge);
            if (dateOfBirth > DateOnly.FromDateTime(utcNow).AddYears(-minimumAge)) 
                throw new ArgumentOutOfRangeException(nameof(dateOfBirth), $"Customer must be at least {minimumAge} years old.");

            return new CustomerProfile(Guid.CreateVersion7(), userId, firstName.Trim(), lastName.Trim(), dateOfBirth);
        }

        /// <summary>Submits the profile for KYC review.</summary>
        public void MarkPending()
        {
            if (KycStatus is not (KycState.Unverified or KycState.Rejected)) throw new InvalidOperationException($"Cannot submit KYC from state {KycStatus}.");

            KycStatus = KycState.Pending;
            VerifiedAt = null;
        }

        /// <summary>Records a successful KYC verification.</summary>
        public void MarkVerified(DateTime verifiedAtUtc)
        {
            if (KycStatus is not KycState.Pending) throw new InvalidOperationException($"Cannot verify from state {KycStatus}.");

            KycStatus = KycState.Verified;
            VerifiedAt = verifiedAtUtc;
        }

        /// <summary>Records a failed KYC verification.</summary>
        public void MarkRejected()
        {
            if (KycStatus is not KycState.Pending) throw new InvalidOperationException($"Cannot reject from state {KycStatus}.");

            KycStatus = KycState.Rejected;
            VerifiedAt = null;
        }
    }
}
