using FluentValidation;
using Limak.Application.DTOs.CategoryDTOs;

namespace Limak.Application.Validators.CategoryValidators;

public class CategoryPutDtoValidator : AbstractValidator<CategoryPutDto>
{
    public CategoryPutDtoValidator()
    {
        RuleFor(x => x.Name).MaximumLength(128).MinimumLength(3).NotNull();

    }
}
