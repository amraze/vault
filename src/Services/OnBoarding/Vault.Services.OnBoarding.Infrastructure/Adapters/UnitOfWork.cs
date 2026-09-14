using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Infrastructure.Persistence;

namespace Vault.Services.OnBoarding.Infrastructure.Adapters
{
    /// <summary>EF Core adapter for <see cref="IUnitOfWork"/>. </summary>
    internal sealed class UnitOfWork(OnBoardingDbContext dbContext) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
    }
}
