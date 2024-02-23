using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderFilterAdminDtoValidator : AbstractValidator<OrderFilterAdminDto>
{
    public OrderFilterAdminDtoValidator()
    {
        RuleFor(x => x.CountryId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.WarehouseId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.StatusId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.KargomatId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AppUserId).GreaterThanOrEqualTo(0);
    }
}
