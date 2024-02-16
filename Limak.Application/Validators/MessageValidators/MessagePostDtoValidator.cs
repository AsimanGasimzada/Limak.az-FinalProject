using FluentValidation;
using Limak.Application.DTOs.MessageDTOs;

namespace Limak.Application.Validators.MessageValidators;

public class MessagePostDtoValidator:AbstractValidator<MessagePostDto>
{
    public MessagePostDtoValidator()
    {
        RuleFor(x=>x.ChatId).NotNull().GreaterThan(0);
        RuleFor(x => x.Body).NotNull().MaximumLength(4098);
        
    }
}
