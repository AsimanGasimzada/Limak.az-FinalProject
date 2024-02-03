using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Repositories;
using Limak.Persistence.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limak.Persistence.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        AddServices(services);
        AddRepositories(services);

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IShopService, ShopService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}
