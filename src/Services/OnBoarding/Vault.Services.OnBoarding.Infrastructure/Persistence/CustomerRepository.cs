using Microsoft.EntityFrameworkCore;
using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Domain.Customer;

namespace Vault.Services.OnBoarding.Infrastructure.Persistence
{
    /// <summary>EF Core adapter for <see cref="ICustomerRepository"/>.</summary>
    internal sealed class CustomerRepository(DbContext dbContext) : ICustomerRepository
    {
        public Task<bool> ExistsForUserAsync(Guid userId, CancellationToken ct = default) =>
            dbContext.Set<CustomerProfile>().AnyAsync(profile => profile.UserId == userId, ct);

        public async Task<Guid> AddAsync(CustomerProfile profile, CancellationToken ct = default)
        {
            await dbContext.Set<CustomerProfile>().AddAsync(profile, ct);
            return profile.Id;
        }
    }
}
