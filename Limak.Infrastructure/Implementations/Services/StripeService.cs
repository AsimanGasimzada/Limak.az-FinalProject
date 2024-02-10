using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.StripeDTOs;
using Stripe;

namespace Limak.Infrastructure.Implementations.Services;

public class StripeService : IPaymentService
{
    public async Task<ResultDto> PayAsync(StripePayDto dto)
    {
        var service = new PaymentIntentService();

        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(dto.Amount * 100),
            Currency = "usd",
            PaymentMethodTypes = new List<string> { "card" },
            PaymentMethod = dto.PublishableToken,
            Confirm = true,
            ReceiptEmail = dto.Email,
            
        };  

        var paymentIntent = await service.CreateAsync(options);
        if (paymentIntent.Status != "succeeded")
        {
            throw new Exception("Payment is failed");
        }
        return new("Payment is successfully");

    }
}
