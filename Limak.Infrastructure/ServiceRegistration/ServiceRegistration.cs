using Limak.Application.Abstractions.Helpers;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.StripeDTOs;
using Limak.Infrastructure.Implementations.Security.JWT;
using Limak.Infrastructure.Implementations.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace Limak.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        AddJwtBearer(services, configuration);
        AddStripe(services, configuration);

        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ITokenHelper, JWTHelper>();
        services.AddScoped<IEmailHelper, MailKitHelper>();
        services.AddScoped<IPaymentService, StripeService>();
        services.AddScoped<StripeSettings>();


        return services;
    }

    private static void AddStripe(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StripeSettings>(configuration.GetSection("StripeOptions"));
        StripeConfiguration.ApiKey = configuration["StripeOptions:SecretKey"];
    }

    private static void AddJwtBearer(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = configuration["TokenOptions:Issuer"],
                ValidAudience = configuration["TokenOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenOptions:SecurityKey"])),
                LifetimeValidator = (_, expired, token, _) => token != null ? expired > DateTime.UtcNow : false


            };
        });
    }
}
