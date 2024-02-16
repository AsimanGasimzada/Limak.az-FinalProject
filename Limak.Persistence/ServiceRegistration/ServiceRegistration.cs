using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Domain.Entities;
using Limak.Persistence.DAL;
using Limak.Persistence.Implementations.Hubs;
using Limak.Persistence.Implementations.Repositories;
using Limak.Persistence.Implementations.Services;
using Limak.Persistence.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Limak.Persistence.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();
        AddDbContext(services, configuration);
        AddIdentity(services);
        AddServices(services);
        AddRepositories(services);
        AddInterceptors(services);


        return services;
    }

    public static WebApplication AddSignalREndpoints(this WebApplication app)
    {
        app.MapHub<NotificationHub>("/notificationHub");
        app.MapHub<ChatHub>("/chatHub");

        return app;
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
        services.AddScoped<ICitizenshipService, CitizenshipService>();
        services.AddScoped<IGenderService, GenderService>();
        services.AddScoped<IUserPositionService, UserPositionService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
        services.AddScoped<IKargomatService, KargomatService>();
        services.AddScoped<IDeliveryAreaService, DeliveryAreaService>();
        services.AddScoped<IDeliveryService, DeliveryService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();

    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IShopCategoryRepository, ShopCategoryRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<ICitizenshipRepository, CitizenshipRepository>();
        services.AddScoped<IGenderRepository, GenderRepository>();
        services.AddScoped<IUserPositionRepository, UserPositionRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        services.AddScoped<IKargomatRepository, KargomatRepository>();
        services.AddScoped<IDeliveryAreaRepository, DeliveryAreaRepository>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<ITariffRepository, TariffRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();


    }
    #endregion
}
