using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderAdminPutDtoValidator : AbstractValidator<OrderAdminPutDto>
{
    public OrderAdminPutDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotNull();
        RuleFor(x => x.AdditionFeesNotes).MaximumLength(256);
        RuleFor(x => x.AdditionFees).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.AdditionFees).NotNull().GreaterThan(0);
    }
}
