using FluentValidation;
using Limak.Application.DTOs.BrandDTOs;

namespace Limak.Application.Validators.BrandValidators;

public class BrandPostDtoValidator:AbstractValidator<BrandPostDto>
{
    public BrandPostDtoValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().MaximumLength(256).MinimumLength(2);
        RuleFor(x=>x.WebsitePath).NotEmpty().MaximumLength(512).MinimumLength(2);
        RuleFor(x=>x.Image).NotEmpty();
    }
}
