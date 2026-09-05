using Microsoft.Extensions.DependencyInjection;

namespace Vault.Services.OnBoarding.Application
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>Registers the application layer: MediatR handlers and pipeline behaviors.</summary>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ApplicationServiceExtensions).Assembly;


            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            return services;
        }
    }
}
