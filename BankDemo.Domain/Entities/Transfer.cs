using BankDemo.Domain.Enums;
using BankDemo.Domain.Exceptions;

namespace BankDemo.Domain.Entities;

public class Transfer
{
    public Guid Id { get; }
    public string CustomerId { get; }
    public string FromAccountId { get; }
    public Guid BeneficiaryId { get; }
    public decimal Amount { get; }
    public Currency Currency { get; }
    public string? Description { get; }
    public TransferStatus Status { get; private set; }
    public string ClientRequestId { get; }
    public DateTime CreatedAt { get; }

    public Transfer(
        Guid id,
        string customerId,
        string fromAccountId,
        Guid beneficiaryId,
        decimal amount,
        Currency currency,
        string? description,
        string clientRequestId,
        DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new DomainException("Customer id is required.");

        if (string.IsNullOrWhiteSpace(fromAccountId))
            throw new DomainException("From account id is required.");

        if (amount <= 0)
            throw new DomainException("Transfer amount must be greater than zero.");

        if (!string.IsNullOrWhiteSpace(description) && description.Length > 140)
            throw new DomainException("Description max length is 140 characters.");

        if (string.IsNullOrWhiteSpace(clientRequestId))
            throw new DomainException("ClientRequestId is required.");

        Id = id;
        CustomerId = customerId;
        FromAccountId = fromAccountId;
        BeneficiaryId = beneficiaryId;
        Amount = amount;
        Currency = currency;
        Description = description;
        ClientRequestId = clientRequestId;
        CreatedAt = createdAt;
        Status = TransferStatus.Completed; // simplificado
    }
}
