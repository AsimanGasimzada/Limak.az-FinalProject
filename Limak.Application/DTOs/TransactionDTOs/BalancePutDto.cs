namespace Limak.Application.DTOs.TransactionDTOs;

public class BalancePutDto
{
    public decimal Amount { get; set; }
    public List<int>? OrderIds { get; set; }
}
