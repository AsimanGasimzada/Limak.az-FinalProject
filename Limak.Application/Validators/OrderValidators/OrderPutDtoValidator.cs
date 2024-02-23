using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderPutDtoValidator : AbstractValidator<OrderPutDto>
{
    public OrderPutDtoValidator()
    {

        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.OrderURL).NotNull().MaximumLength(1024).MinimumLength(5);
        RuleFor(x => x.Price).NotNull().GreaterThan(0);
        RuleFor(x => x.LocalCargoPrice).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Count).NotNull().GreaterThan(0);
        RuleFor(x => x.Notes).NotNull().MaximumLength(256).MinimumLength(1);
    }
}
