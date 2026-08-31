using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Vault.Services.OnBoarding.Application.Abstractions;

namespace Vault.Services.OnBoarding.Application
{
    public static class DependencyInjection
    {
        /// <summary>Registers the OnBoarding application layer: MediatR handlers and pipeline behaviors.</summary>
        public static IServiceCollection AddOnBoardingApplication(this IServiceCollection services, string? mediatRLicenseKey = null)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IMediator).Assembly);
            });

            return services;
        }
    }
}
