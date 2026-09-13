using Vault.Services.OnBoarding.Application.Ports;

namespace Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence
{
    /// <summary>EF Core adapter for <see cref="IUnitOfWork"/>. </summary>
    internal sealed class UnitOfWork(OnBoardingDbContext dbContext) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
    }
}
