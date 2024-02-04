using FluentValidation;
using Limak.Application.DTOs.KargomatDTOs;

namespace Limak.Application.Validators.KargomatValidators;

public class KargomatPostDtoValidator:AbstractValidator<KargomatPostDto>
{
    public KargomatPostDtoValidator()
    {
        RuleFor(x => x.Location).NotNull().MaximumLength(256).MinimumLength(3);
        RuleFor(x => x.Position).NotNull().MaximumLength(256).MinimumLength(3);
        RuleFor(x => x.Price).NotNull();
    }
}
