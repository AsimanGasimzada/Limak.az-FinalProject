using FluentValidation;
using Limak.Application.DTOs.ShopDTOs;

namespace Limak.Application.Validators.ShopValidators;

public class ShopPutDtoValidator : AbstractValidator<ShopPutDto>
{
    public ShopPutDtoValidator()
    {
        RuleFor(x => x.Name).MaximumLength(256).MinimumLength(1).NotNull();
        RuleFor(x=>x.CategoryIds).NotNull();
    }
}
