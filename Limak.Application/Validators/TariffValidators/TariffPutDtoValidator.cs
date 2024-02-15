using FluentValidation;
using Limak.Application.DTOs.TariffDTOs;

namespace Limak.Application.Validators.TariffValidators;

public class TariffPutDtoValidator : AbstractValidator<TariffPutDto>
{
    public TariffPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.CountryId).NotNull();
        RuleFor(x => x.MinValue).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.MaxValue).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.MaxValue)
            .Must((dto, maxValue) => maxValue > dto.MinValue)
            .WithMessage("Max value must be greater than Min value.");
        RuleFor(x => x.Price).NotNull().GreaterThanOrEqualTo(0);
    }
}
