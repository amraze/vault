using Vault.Services.OnBoarding.Domain.Customer;

namespace Vault.Services.OnBoarding.Application.Ports
{
    /// <summary>
    /// Persistence port for the CustomerProfile aggregate. Methods stage work only;
    /// </summary>
    public interface ICustomerProfileRepository
    {
        /// <summary>
        /// True when the login already has a profile. This is for a clean 409 - the unique
        /// </summary>
        Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>Stages a new profile for insert.</summary>
        Task<Guid> AddAsync(CustomerProfile profile, CancellationToken cancellationToken = default);
    }
}
