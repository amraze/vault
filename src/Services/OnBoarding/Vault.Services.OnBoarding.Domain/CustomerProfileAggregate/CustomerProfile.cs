using Vault.Services.OnBoarding.Domain.Abstractions;
using Vault.Services.OnBoarding.Domain.CustomerProfileAggregate.Enums;
using Vault.Services.OnBoarding.Domain.CustomerProfileAggregate.Events;
using Vault.Services.OnBoarding.Domain.Exceptions;

namespace Vault.Services.OnBoarding.Domain.CustomerProfileAggregate
{
    /// <summary>Aggregate Root for customer profiles aggregate : Register(), MarkPending(), MarkVerified(), MarkRejected()</summary>
    public sealed class CustomerProfile : AggregateRoot<Guid>
    {
        private CustomerProfile() { }   // EF

        private CustomerProfile(Guid id, Guid userId, string firstName, string lastName, DateOnly dateOfBirth): base(id)
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
        public DateTimeOffset? VerifiedAt { get; private set; }

        /// <summary>Registers a new, unverified customer profile.</summary>
        public static CustomerProfile Register(Guid userId, string firstName, string lastName, DateOnly dateOfBirth, DateTimeOffset now, int minimumAge)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId is required.", nameof(userId));
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentOutOfRangeException.ThrowIfNegative(minimumAge);
            if (dateOfBirth > DateOnly.FromDateTime(now.UtcDateTime).AddYears(-minimumAge))
                throw new DomainException($"Customer must be at least {minimumAge} years old.");

            var profile = new CustomerProfile(Guid.CreateVersion7(), userId, firstName.Trim(), lastName.Trim(), dateOfBirth);

            profile.RaiseEvent(new CustomerProfileRegistered(profile.Id, userId, now));
            return profile;
        }

        /// <summary>Submits the profile for KYC review.</summary>
        public void MarkPending(DateTimeOffset now)
        {
            if (KycStatus is not (KycState.Unverified or KycState.Rejected))
                throw new ConflictException("submit KYC", KycStatus);

            KycStatus = KycState.Pending;
            VerifiedAt = null;
            RaiseEvent(new KycSubmitted(Id, now));
        }

        /// <summary>Records a successful KYC verification.</summary>
        public void MarkVerified(DateTimeOffset verifiedAt)
        {
            if (KycStatus is not KycState.Pending)
                throw new ConflictException("verify", KycStatus);

            KycStatus = KycState.Verified;
            VerifiedAt = verifiedAt;
            RaiseEvent(new KycVerified(Id, verifiedAt));
        }

        /// <summary>Records a failed KYC verification.</summary>
        public void MarkRejected(DateTimeOffset now)
        {
            if (KycStatus is not KycState.Pending)
                throw new ConflictException("reject", KycStatus);

            KycStatus = KycState.Rejected;
            VerifiedAt = null;
            RaiseEvent(new KycRejected(Id, now));
        }
    }
}
