using Limak.Application.DTOs.OrderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IOrderService
{
    Task<List<OrderGetDto>> GetAllAsync();
    Task<List<OrderGetDto>> GetNotPaymentOrders();
    Task<List<OrderGetDto>> GetUserAllOrders();

    Task<OrderGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(OrderPostDto dto);
    Task<ResultDto> UpdateAsync(OrderPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
    Task<ResultDto> PayOrders(List<int> orderIds);
}
