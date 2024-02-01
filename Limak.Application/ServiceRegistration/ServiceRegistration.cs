using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Limak.Application.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
        return service;
    }
}
