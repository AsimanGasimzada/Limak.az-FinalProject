using FluentValidation;
using Limak.Application.DTOs.CategoryDTOs;

namespace Limak.Application.Validators.CategoryValidators;

public class CategoryPostValidator:AbstractValidator<CategoryPostDto>
{
    public CategoryPostValidator()
    {
        RuleFor(x => x.Name).MaximumLength(128).MinimumLength(3).NotNull();
        RuleFor(x => x.Image).NotNull();

    }
}
