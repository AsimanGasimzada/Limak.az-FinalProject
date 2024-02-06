using FluentValidation;
using Limak.Application.DTOs.DeliveryAreaDTOs;

namespace Limak.Application.Validators.DeliveryAreaValidators;

public class DeliveryAreaPostDtoValidator:AbstractValidator<DeliveryAreaPostDto>
{
    public DeliveryAreaPostDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(256).MinimumLength(2);
        RuleFor(x => x.Price).NotNull();
        RuleFor(x => x.WarehouseId).NotNull();
    }
}
