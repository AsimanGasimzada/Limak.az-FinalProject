using FluentValidation;
using Limak.Application.DTOs.DeliveryAreaDTOs;

namespace Limak.Application.Validators.DeliveryAreaValidators;

public class DeliveryAreaPutDtoValidator:AbstractValidator<DeliveryAreaPutDto>
{
	public DeliveryAreaPutDtoValidator()
	{
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Name).NotNull().MaximumLength(256).MinimumLength(2);
        RuleFor(x => x.Price).NotNull().GreaterThan(0);
        RuleFor(x => x.WarehouseId).NotNull().GreaterThan(0);
    }
}
