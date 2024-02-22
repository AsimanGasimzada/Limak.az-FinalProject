using FluentValidation;
using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.Validators.OrderValidators;

public class OrderAdminPostDtoValidator : AbstractValidator<OrderAdminPostDto>
{
    public OrderAdminPostDtoValidator()
    {
        RuleFor(x => x.OrderURL).NotNull().MaximumLength(1024).MinimumLength(5);
        RuleFor(x => x.Price).NotNull();
        RuleFor(x => x.LocalCargoPrice).NotNull();
        RuleFor(x => x.Count).NotNull();
        RuleFor(x => x.Color).NotNull().MaximumLength(128).MinimumLength(1);
        RuleFor(x => x.Size).NotNull().MaximumLength(128).MinimumLength(1);
        RuleFor(x => x.Notes).NotNull().MaximumLength(256).MinimumLength(1);
        RuleFor(x => x.WarehouseId).NotNull();
        RuleFor(x=>x.Weight).NotNull().GreaterThan(0);
        RuleFor(x=>x.UserName).NotNull();
    }

}

