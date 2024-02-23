using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderFilterDtoValidator:AbstractValidator<OrderFilterDto>
{
    public OrderFilterDtoValidator()
    {
        RuleFor(x => x.CountryId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.WarehouseId).GreaterThanOrEqualTo(0);
        RuleFor(x=>x.StatusId).GreaterThanOrEqualTo(0);
        RuleFor(x=>x.KargomatId).GreaterThanOrEqualTo(0);
    }
}
