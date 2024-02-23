using FluentValidation;
using Limak.Application.DTOs.CitizenshipDTOs;

namespace Limak.Application.Validators.CitizenshipValidators;

public class CitizenshipPutDtoValidator:AbstractValidator<CitizenshipPutDto>
{
    public CitizenshipPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(3);
    }
}
