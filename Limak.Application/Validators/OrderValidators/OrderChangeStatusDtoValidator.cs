using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderChangeStatusDtoValidator : AbstractValidator<OrderChangeStatusDto>
{
    public OrderChangeStatusDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.StatusId).NotNull();
    }

}
