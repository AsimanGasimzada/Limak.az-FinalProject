using Limak.Application.Abstractions.Services;
using Limak.Infrastructure.Implementations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Limak.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        return services;
    }
}
