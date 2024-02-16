using FluentValidation.AspNetCore;
using Limak.Application.Validators.ShopValidators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Limak.Application.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(ShopPostDtoValidator)));

        return services;
    }
}
