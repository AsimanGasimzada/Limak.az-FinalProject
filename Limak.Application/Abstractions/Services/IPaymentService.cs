using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.StripeDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IPaymentService
{
    Task<ResultDto> PayAsync(StripePayDto dto);
    string GetPublishableKey();
}
