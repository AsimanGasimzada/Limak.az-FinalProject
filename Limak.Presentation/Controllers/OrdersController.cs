using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("orders")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }


    [HttpGet("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetUserAllOrders());
    }


    [HttpGet("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetNotPaymeyntOrders()
    {
        return Ok(await _service.GetNotPaymentOrders());
    }


    [HttpPost("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> PayOrders([FromForm] List<int> orderIds)
    {
        return Ok(await _service.PayOrders(orderIds));
    }


    [HttpPost("[action]/{id}")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> PayOrder([FromRoute]int id)
    {
        return Ok(await _service.PayFullOrder(id));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(OrderPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {

        return Ok(await _service.DeleteAsync(id));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] OrderPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }

    [HttpPatch("[action]")]
    public async Task<IActionResult> SetKargomat(OrderSetKargomatDto dto)
    {
        return Ok(await _service.SetKargomatAsync(dto));
    }




    //Admin

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetAllAdminAsync()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> PutOrderAdminAsync(OrderAdminPutDto dto)
    {
        return Ok(await _service.UpdateOrderByAdminAsync(dto));
    }

    [HttpPatch("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> ChangeOrderStatusAsync(OrderChangeStatusDto dto)
    {
        return Ok(await _service.ChangeOrderStatusAsync(dto));
    }

    [HttpPatch("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> OrderCancel(OrderCancelDto dto)
    {
        return Ok(await _service.OrderCancelAsync(dto));
    }



    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> CancelKargomat([FromRoute] int id)
    {
        return Ok(await _service.CancelKargomatAsync(id));
    }


    [HttpPost("[action]")]
    [Authorize(Roles ="Admin,Moderator")]
    public async Task<IActionResult> CreateByAdmin(OrderAdminPostDto dto)
    {
        return Ok(await _service.CreateByAdminAsync(dto));
    }
}
