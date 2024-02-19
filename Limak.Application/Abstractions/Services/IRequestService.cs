using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IRequestService
{
    Task<ResultDto> CreateAsync(RequestPostDto dto);
    Task<ResultDto> UpdateAsync(RequestPutDto dto);
    Task<List<RequestGetDto>> GetUsersAllRequestsAsync();
    Task<List<RequestGetDto>> GetAllAsync();
}
