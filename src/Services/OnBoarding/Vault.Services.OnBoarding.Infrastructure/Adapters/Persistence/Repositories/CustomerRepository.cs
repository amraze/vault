using Microsoft.EntityFrameworkCore;
using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Domain.Customer;
using Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence.Models;

namespace Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence.Repositories
{
    /// <summary>EF Core adapter for <see cref="ICustomerRepository"/>.</summary>
    internal sealed class CustomerRepository(OnBoardingDbContext dbContext) : ICustomerRepository
    {
        public Task<bool> ExistsForUserAsync(Guid userId, CancellationToken ct = default) =>
            dbContext.Set<PersistenceCustomerProfile>().AnyAsync(cp => cp.UserId == userId, ct);

        public async Task<Guid> AddAsync(CustomerProfile cp, CancellationToken ct = default)
        {
            var pcp = new PersistenceCustomerProfile
            {
                UserId = cp.UserId,
                FirstName = cp.FirstName,
                LastName = cp.LastName,
                DateOfBirth = cp.DateOfBirth,
                KycStatus = cp.KycStatus,
            };

            await dbContext.Set<PersistenceCustomerProfile>().AddAsync(pcp, ct);
            return cp.Id;
        }
    }
}
