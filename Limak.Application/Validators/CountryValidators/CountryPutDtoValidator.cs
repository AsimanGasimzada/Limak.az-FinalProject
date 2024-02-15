using FluentValidation;
using Limak.Application.DTOs.CountryDTOs;

namespace Limak.Application.Validators.CountryValidators;

public class CountryPutDtoValidator:AbstractValidator<CountryPutDto>
{
    public CountryPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();   
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(3);
    }
}
