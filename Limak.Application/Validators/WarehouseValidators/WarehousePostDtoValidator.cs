using FluentValidation;
using Limak.Application.DTOs.WarehouseDTOs;

namespace Limak.Application.Validators.WarehouseValidators;

public class WarehousePostDtoValidator:AbstractValidator<WarehousePostDto>
{
    public WarehousePostDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Location).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Position).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.PhoneNumber).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Email).NotNull().MaximumLength(256).MinimumLength(5);
        RuleFor(x => x.WorkingHours).NotNull().MaximumLength(256).MinimumLength(3);
    }
}
