using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderPostDtoValidator : AbstractValidator<OrderPostDto>
{
    public OrderPostDtoValidator()
    {
        RuleFor(x => x.OrderURL).NotNull().MaximumLength(1024).MinimumLength(5);
        RuleFor(x => x.Price).NotNull();
        RuleFor(x => x.LocalCargoPrice).NotNull();
        RuleFor(x => x.Count).NotNull();
        RuleFor(x => x.Color).NotNull().MaximumLength(128).MinimumLength(1);
        RuleFor(x => x.Size).NotNull().MaximumLength(128).MinimumLength(1);
        RuleFor(x => x.Notes).NotNull().MaximumLength(256).MinimumLength(1);
        RuleFor(x => x.WarehouseId).NotNull();
    }

}

