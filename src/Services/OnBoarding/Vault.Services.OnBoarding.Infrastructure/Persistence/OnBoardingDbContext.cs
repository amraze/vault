using Microsoft.EntityFrameworkCore;
using Vault.Services.OnBoarding.Infrastructure.Persistence.Models;

namespace Vault.Services.OnBoarding.Infrastructure.Persistence
{
    internal sealed class OnBoardingDbContext(DbContextOptions<OnBoardingDbContext> options) : DbContext(options)
    {
        internal DbSet<PersistenceCustomerProfile> CustomerProfiles => Set<PersistenceCustomerProfile>();
    }
}
