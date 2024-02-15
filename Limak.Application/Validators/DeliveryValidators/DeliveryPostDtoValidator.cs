using FluentValidation;
using Limak.Application.DTOs.DeliveryDTOs;

namespace Limak.Application.Validators.DeliveryValidators;

public class DeliveryPostDtoValidator : AbstractValidator<DeliveryPostDto>
{
    public DeliveryPostDtoValidator()
    {
        RuleFor(x => x.Region).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Village).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Street).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.HomeNo).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Phone).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.OrderIds).NotNull();
        RuleFor(x => x.DeliveryAreaId).NotNull();



    }
}