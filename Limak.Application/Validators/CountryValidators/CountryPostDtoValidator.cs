using FluentValidation;
using Limak.Application.DTOs.CountryDTOs;

namespace Limak.Application.Validators.CountryValidators;

public class CountryPostDtoValidator:AbstractValidator<CountryPostDto>
{
    public CountryPostDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(3);
    }
}
