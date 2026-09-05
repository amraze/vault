namespace Vault.Services.OnBoarding.Application.Ports
{
    /// <summary>
    /// Commits everything staged during the current request as a single transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
