using FluentValidation;
using Limak.Application.DTOs.ChatDTOs;

namespace Limak.Application.Validators.ChatValidators;

public class ChatPutDtoValidator:AbstractValidator<ChatPutDto>
{
    public ChatPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Feedback).NotNull().MaximumLength(2048);
    }
}
