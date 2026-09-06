using Microsoft.EntityFrameworkCore;
using Vault.Services.OnBoarding.Application.Ports;

namespace Vault.Services.OnBoarding.Infrastructure.Persistence
{
    /// <summary>EF Core adapter for <see cref="IUnitOfWork"/>. </summary>
    internal sealed class UnitOfWork(DbContext dbContext) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
    }
}
