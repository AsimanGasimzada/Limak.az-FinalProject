using FluentValidation;
using Limak.Application.DTOs.UserPositionDTOs;

namespace Limak.Application.Validators.UserPositionValidators;

public class UserPositionPostDtoValidator:AbstractValidator<UserPositionPostDto>
{
    public UserPositionPostDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(3);
    }
}
