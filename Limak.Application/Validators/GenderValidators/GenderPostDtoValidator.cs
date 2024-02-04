using FluentValidation;
using Limak.Application.DTOs.GenderDTOs;

namespace Limak.Application.Validators.GenderValidators;

public class GenderPostDtoValidator:AbstractValidator<GenderPostDto>
{
    public GenderPostDtoValidator()
    {
        RuleFor(x=>x.Name).NotNull().MaximumLength(64).MinimumLength(2);
    }
}
