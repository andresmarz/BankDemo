using BankDemo.Domain.Enums;

namespace BankDemo.API.DTOs.Transfers;

public class CreateTransferRequest
{
    public string CustomerId { get; set; } = null!;
    public string ClientRequestId { get; set; } = null!;
    public string FromAccountId { get; set; } = null!;
    public Guid BeneficiaryId { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public string? Description { get; set; }
}
