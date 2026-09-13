using Microsoft.Extensions.DependencyInjection;
using Vault.Services.OnBoarding.Application.Ports;
using Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence;
using Vault.Services.OnBoarding.Infrastructure.Adapters.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Vault.Services.OnBoarding.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        /// <summary>Registers the infrastructure layer: repositories and other dependencies.</summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionStringAttr = "DefaultConnection")
        {
            services.AddDbContextFactory<OnBoardingDbContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(connectionStringAttr) ?? throw new ArgumentNullException(connectionStringAttr, $"Connection string '{connectionStringAttr}' was not found in configuration.");

                options.UseNpgsql(connectionString);
            }, ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}