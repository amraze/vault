using Microsoft.EntityFrameworkCore;
using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Domain.Customer;
using Vault.Services.OnBoarding.Infrastructure.Persistence;
using Vault.Services.OnBoarding.Infrastructure.Persistence.Models;

namespace Vault.Services.OnBoarding.Infrastructure.Adapters.Repositories
{
    /// <summary>EF Core adapter for <see cref="ICustomerRepository"/>.</summary>
    internal sealed class CustomerCommandRepository(OnBoardingDbContext dbContext) : ICustomerCommandRepository
    {
        public async Task<Guid> AddAsync(CustomerProfile cp, CancellationToken ct = default)
        {
            var cpe = new CustomerProfileEntity
            {
                UserId = cp.UserId,
                FirstName = cp.FirstName,
                LastName = cp.LastName,
                DateOfBirth = cp.DateOfBirth,
                KycStatus = cp.KycStatus,
            };

            await dbContext.Set<CustomerProfileEntity>().AddAsync(cpe, ct);
            return cp.Id;
        }
    }

    internal sealed class CustomerQueryRepository(OnBoardingDbContext dbContext) : ICustomerQueryRepository
    {
        public Task<bool> ExistsForUserAsync(Guid userId, CancellationToken ct = default) =>
            dbContext.Set<CustomerProfileEntity>().AnyAsync(cp => cp.UserId == userId, ct);
    }

}
