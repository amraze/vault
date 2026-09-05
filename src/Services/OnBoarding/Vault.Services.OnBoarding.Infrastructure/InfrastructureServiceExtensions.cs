using Microsoft.Extensions.DependencyInjection;

namespace Vault.Services.OnBoarding.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        /// <summary>Registers the infrastructure layer: repositories and other dependencies.</summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register infrastructure services here
            // Example: services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}           