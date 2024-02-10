using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderChangeStatusDtoValidator : AbstractValidator<OrderChangeStatusDto>
{
    public OrderChangeStatusDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.StatusId).NotNull();
    }

}
