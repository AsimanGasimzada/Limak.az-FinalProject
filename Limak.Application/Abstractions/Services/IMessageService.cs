using Limak.Application.DTOs.MessageDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IMessageService
{
    Task<ResultDto> SendMessageAsync(MessagePostDto dto);

}
