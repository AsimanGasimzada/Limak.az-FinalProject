using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderPutDtoValidator : AbstractValidator<OrderPutDto>
{
    public OrderPutDtoValidator()
    {

        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.OrderURL).NotNull().MaximumLength(1024).MinimumLength(5);
        RuleFor(x => x.Price).NotNull();
        RuleFor(x => x.LocalCargoPrice).NotNull();
        RuleFor(x => x.Count).NotNull();
        RuleFor(x => x.Notes).NotNull().MaximumLength(256).MinimumLength(1);
    }
}
