using FluentValidation;
using Limak.Application.DTOs.StatusDTOs;

namespace Limak.Application.Validators.StatusValidators;
public class StatusPostDtoValidator :AbstractValidator<StatusPostDto>
{
    public StatusPostDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(128).MinimumLength(2);
    }
}
