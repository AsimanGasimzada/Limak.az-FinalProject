namespace Limak.Application.DTOs.TransactionDTOs;

public class TransactionGetDto
{
    public decimal Amount { get; set; }
    public bool IsPayment { get; set; }
    public string Feedback { get; set; } = null!;
    public decimal UserBalance { get; set; }
    public int AppUserId { get; set; }
}
