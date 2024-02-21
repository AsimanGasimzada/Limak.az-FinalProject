namespace Limak.Application.DTOs.TransactionDTOs;

public class GetBalanceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public decimal AZNBalance { get; set; }
    public decimal TRYBalance { get; set; }
    public decimal USDBalance { get; set; }
}
