using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Domain.Entities;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Repositories;
using Limak.Persistence.Implementations.Services;
using Limak.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limak.Persistence.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        AddDbContext(services, configuration);
        AddIdentity(services);
        AddServices(services);
        AddRepositories(services);
        AddInterceptors(services);


        return services;
    }


    #region PrivateMethods

    private static void AddInterceptors(IServiceCollection services)
    {
        services.AddScoped<BaseEntityInterceptor>();
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>(opt =>
        {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = true;
            opt.Lockout.AllowedForNewUsers = false;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            opt.Lockout.MaxFailedAccessAttempts = 3;
            
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IShopService, ShopService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IShopCategoryService, ShopCategoryService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IAuthService, AuthService>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IShopCategoryRepository, ShopCategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
    }
    #endregion
}
