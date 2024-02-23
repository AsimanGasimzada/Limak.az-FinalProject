using FluentValidation;
using Limak.Application.DTOs.SettingDTOs;

namespace Limak.Application.Validators.SettingValidators;

public class SettingPutDtoValidator : AbstractValidator<SettingPutDto>
{
    public SettingPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Value).NotNull().MaximumLength(1024).MinimumLength(1);
    }
}
