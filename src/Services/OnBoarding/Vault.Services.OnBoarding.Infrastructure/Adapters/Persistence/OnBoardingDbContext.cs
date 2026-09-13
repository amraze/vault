using Microsoft.EntityFrameworkCore;
using Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence.Models;

namespace Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence
{
    internal sealed class OnBoardingDbContext(DbContextOptions<OnBoardingDbContext> options) : DbContext(options)
    {
        internal DbSet<PersistenceCustomerProfile> CustomerProfiles => Set<PersistenceCustomerProfile>();
    }
}
