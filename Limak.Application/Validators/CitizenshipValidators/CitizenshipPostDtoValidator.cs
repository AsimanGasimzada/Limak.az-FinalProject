using FluentValidation;
using Limak.Application.DTOs.CitizenshipDTOs;

namespace Limak.Application.Validators.CitizenshipValidators;

public class CitizenshipPostDtoValidator : AbstractValidator<CitizenshipPostDto>
{
    public CitizenshipPostDtoValidator()
    {
        RuleFor(x=>x.Name).NotEmpty().MaximumLength(64).MinimumLength(3);
    }   
}
