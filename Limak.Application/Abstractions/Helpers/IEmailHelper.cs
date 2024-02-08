using Limak.Application.DTOs.Common;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Helpers;

public interface IEmailHelper
{
    Task<ResultDto> SendEmailAsync(MailRequestDto mailRequestDto);

}
