using FluentValidation;
using Limak.Application.DTOs.KargomatDTOs;

namespace Limak.Application.Validators.KargomatValidators;

public class KargomatPutDtoValidator:AbstractValidator<KargomatPutDto>
{
    public KargomatPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Location).NotNull().MaximumLength(256).MinimumLength(3);
        RuleFor(x => x.CordinateX).NotNull().MaximumLength(256).MinimumLength(3);
        RuleFor(x => x.CordinateY).NotNull().MaximumLength(256).MinimumLength(3);
        RuleFor(x => x.Price).NotNull().GreaterThan(0);
    }
}
