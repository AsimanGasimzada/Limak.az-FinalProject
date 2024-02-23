using FluentValidation;
using Limak.Application.DTOs.SettingDTOs;

namespace Limak.Application.Validators.SettingValidators;

public class SettingPostDtoValidator:AbstractValidator<SettingPostDto>
{
    public SettingPostDtoValidator()
    {
        RuleFor(x => x.Value).NotNull().MaximumLength(1024).MinimumLength(1);
        RuleFor(x => x.Key).NotNull().MaximumLength(128).MinimumLength(1);
    }
}
