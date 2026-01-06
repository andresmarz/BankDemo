using BankDemo.Domain.Enums;

namespace BankDemo.Application.Transfers.CreateTransfer;

public class CreateTransferCommand
{
    public string CustomerId { get; init; } = null!;
    public string ClientRequestId { get; init; } = null!;
    public string FromAccountId { get; init; } = null!;
    public Guid BeneficiaryId { get; init; }
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }
    public string? Description { get; init; }
}
