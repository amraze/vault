using Vault.Services.OnBoarding.Domain.Customer;

namespace Vault.Services.OnBoarding.Application.Ports
{
    /// <summary>
    /// Persistence port for the CustomerProfile aggregate. Methods stage work only;
    /// </summary>
    public interface ICustomerCommandRepository
    {
        /// <summary>Stages a new profile for insert.</summary>
        Task<Guid> AddAsync(CustomerProfile profile, CancellationToken cancellationToken = default);
    }

    public interface ICustomerQueryRepository
    {
        /// <summary>Checks if a profile exists for the given user.</summary>
        Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
