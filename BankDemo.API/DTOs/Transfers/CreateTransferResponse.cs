namespace BankDemo.API.DTOs.Transfers;

public class CreateTransferResponse
{
    public Guid TransferId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
