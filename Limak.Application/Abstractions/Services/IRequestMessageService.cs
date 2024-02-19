using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestMessageDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IRequestMessageService
{
    Task<ResultDto> SendAsync(RequestMessagePostDto dto);
}
