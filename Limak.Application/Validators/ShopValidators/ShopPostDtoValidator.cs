using FluentValidation;
using Limak.Application.DTOs.ShopDTOs;

namespace Limak.Application.Validators.ShopValidators;

public class ShopPostDtoValidator:AbstractValidator<ShopPostDto>
{
    public ShopPostDtoValidator()
    {
        RuleFor(x=>x.Name).MaximumLength(256).MinimumLength(1).NotNull();
        RuleFor(x=>x.Image).NotNull();
        RuleFor(x=>x.WebsitePath).NotNull().MaximumLength(512).MinimumLength(1);
        RuleFor(x=>x.CategoryIds).NotNull();
        RuleFor(x => x.CountryId).NotNull().GreaterThan(0);


    }
}
