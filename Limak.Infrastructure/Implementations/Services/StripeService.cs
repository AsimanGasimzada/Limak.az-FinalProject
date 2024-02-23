using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.StripeDTOs;
using Stripe;

namespace Limak.Infrastructure.Implementations.Services;

public class StripeService : IPaymentService
{
    private readonly StripeSettings _settings;
    private readonly INotificationService _notificationService;
    private readonly IAuthService _authService;

    public StripeService(StripeSettings settings, INotificationService notificationService, IAuthService authService)
    {
        _settings = settings;
        _notificationService = notificationService;
        _authService = authService;
    }


    public string GetPublishableKey()
    {
        return _settings.PublishableKey;
    }


    public async Task<ResultDto> PayAsync(StripePayDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();

        var service = new PaymentIntentService();

        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(dto.Amount * 100),
            Currency = "usd",
            PaymentMethodTypes = new List<string> { "card" },
            PaymentMethod = "pm_card_visa",
            Confirm = true,
            ReceiptEmail = dto.Email,

        };

        var paymentIntent = await service.CreateAsync(options);
        if (paymentIntent.Status != "succeeded")
        {
            await _notificationService.CreateAsync(new() { AppUserId = user.Id, Subject = "Uğursuz əməliyyat", Title = "Balans artımınız uğursuz oldu yenidən sınayın." });
            throw new Exception("Payment is failed");
        }
        await _notificationService.CreateAsync(new() { AppUserId = user.Id, Subject = "Balans artımı", Title = "Balans artımınız uğurlu oldu xoş xərcləmələr." });
        return new("Payment is successfully");

    }
}
