using FluentValidation;
using Limak.Application.DTOs.RequestDTOs;

namespace Limak.Application.Validators.RequestValidators;

public class RequestPostDtoValidator : AbstractValidator<RequestPostDto>
{
    public RequestPostDtoValidator()
    {
        RuleFor(x => x.RequestSubjectId).NotNull();
        RuleFor(x => x.CountryId).NotNull();

    }
}
