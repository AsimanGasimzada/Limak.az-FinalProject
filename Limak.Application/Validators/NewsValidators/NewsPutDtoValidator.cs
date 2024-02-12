using FluentValidation;
using Limak.Application.DTOs.NewsDTOs;

namespace Limak.Application.Validators.NewsValidators;

public class NewsPutDtoValidator : AbstractValidator<NewsPutDto>
{
    public NewsPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Subject).NotNull().MinimumLength(3).MaximumLength(128);
        RuleFor(x => x.Description).NotNull().MinimumLength(3).MaximumLength(4098);
    }
}
