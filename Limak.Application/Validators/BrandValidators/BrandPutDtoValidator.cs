using FluentValidation;
using Limak.Application.DTOs.BrandDTOs;

namespace Limak.Application.Validators.BrandValidators;

public class BrandPutDtoValidator : AbstractValidator<BrandPutDto>
{
    public BrandPutDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(256).MinimumLength(2);
        RuleFor(x => x.WebsitePath).NotEmpty().MaximumLength(512).MinimumLength(2);
        RuleFor(x => x.Id).NotEmpty();
    }
}
