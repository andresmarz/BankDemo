using BankDemo.Domain.Enums;
using BankDemo.Domain.Exceptions;

namespace BankDemo.Domain.Entities;

public class Account
{
    public string Id { get; }
    public string CustomerId { get; }
    public Currency Currency { get; }
    public decimal AvailableBalance { get; }

    public Account(string id, string customerId, Currency currency, decimal availableBalance)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new DomainException("Account id is required.");

        if (string.IsNullOrWhiteSpace(customerId))
            throw new DomainException("Customer id is required.");

        if (availableBalance < 0)
            throw new DomainException("Available balance cannot be negative.");

        Id = id;
        CustomerId = customerId;
        Currency = currency;
        AvailableBalance = availableBalance;
    }
}
