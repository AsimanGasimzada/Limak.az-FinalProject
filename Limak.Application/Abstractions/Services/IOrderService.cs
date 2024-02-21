using Limak.Application.DTOs.OrderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface IOrderService
{
    Task<List<OrderGetDto>> GetAllAsync();
    Task<List<OrderGetDto>> GetNotPaymentOrders();
    Task<List<OrderGetDto>> GetUserAllOrders();

    Task<OrderGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(OrderPostDto dto);
    Task<ResultDto> UpdateAsync(OrderPutDto dto);
    Task<bool> IsExistAsync(int id);
    Task<ResultDto> DeleteAsync(int id);
    Task<ResultDto> PayOrders(List<int> orderIds);
    Task<ResultDto> UpdateOrderByAdminAsync(OrderAdminPutDto dto);
    Task<ResultDto> ChangeOrderStatusAsync(OrderChangeStatusDto dto);
    Task<ResultDto> OrderCancelAsync(OrderCancelDto dto);
    Task<ResultDto> SetDelivery(List<int> orderIds, Delivery Delivery);
    Task<ResultDto> CancelDelivery(Delivery delivery);
    Task<ResultDto> SetKargomatAsync(OrderSetKargomatDto dto);
    Task<ResultDto> CancelKargomatAsync(int orderId);
}
