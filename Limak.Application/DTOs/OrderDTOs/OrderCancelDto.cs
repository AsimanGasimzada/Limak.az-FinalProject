using Limak.Domain.Entities;

namespace Limak.Application.DTOs.OrderDTOs;

public class OrderCancelDto
{
    public int Id { get; set; }
    public string CancellationNotes { get; set; }
}
