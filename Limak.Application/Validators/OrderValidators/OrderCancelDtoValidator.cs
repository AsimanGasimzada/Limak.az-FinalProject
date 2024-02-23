using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderCancelDtoValidator : AbstractValidator<OrderCancelDto>
{
    public OrderCancelDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.CancellationNotes).NotNull().MaximumLength(256).MinimumLength(3);
    }

}
