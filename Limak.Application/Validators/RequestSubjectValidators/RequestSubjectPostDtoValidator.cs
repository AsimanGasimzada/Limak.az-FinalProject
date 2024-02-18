using FluentValidation;
using Limak.Application.DTOs.RequestSubjectDTOs;

namespace Limak.Application.Validators.RequestSubjectValidators;

public class RequestSubjectPostDtoValidator : AbstractValidator<RequestSubjectPostDto>
{
    public RequestSubjectPostDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(128).MinimumLength(3);
    }
}
