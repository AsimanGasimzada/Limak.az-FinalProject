using FluentValidation;
using Limak.Application.DTOs.CategoryDTOs;

namespace Limak.Application.Validators.CountryValidators;

public class CountryPutDtoValidator:AbstractValidator<CategoryPutDto>
{
    public CountryPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();   
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(3);   
    }
}
