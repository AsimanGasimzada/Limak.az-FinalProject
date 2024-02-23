using FluentValidation;
using Limak.Application.DTOs.RequestDTOs;

namespace Limak.Application.Validators.RequestValidators;

public class RequestPostDtoValidator : AbstractValidator<RequestPostDto>
{
    public RequestPostDtoValidator()
    {
        RuleFor(x => x.RequestSubjectId).NotNull().GreaterThan(0);
        RuleFor(x => x.CountryId).NotNull().GreaterThan(0);

    }
}
