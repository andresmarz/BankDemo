using BankDemo.Domain.Enums;
using BankDemo.Domain.Exceptions;

namespace BankDemo.Domain.Entities;

public class Beneficiary
{
    public Guid Id { get; }
    public string CustomerId { get; }
    public string Alias { get; private set; }
    public string BankCode { get; private set; }
    public string AccountNumber { get; private set; }
    public Currency Currency { get; private set; }
    public bool IsActive { get; private set; }

    public Beneficiary(
        Guid id,
        string customerId,
        string alias,
        string bankCode,
        string accountNumber,
        Currency currency,
        bool isActive)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new DomainException("Customer id is required.");

        Update(alias, bankCode, accountNumber, currency);

        Id = id;
        CustomerId = customerId;
        IsActive = isActive;
    }

    public void Update(
        string alias,
        string bankCode,
        string accountNumber,
        Currency currency)
    {
        if (string.IsNullOrWhiteSpace(alias) || alias.Length > 30)
            throw new DomainException("Alias is required and must be max 30 characters.");

        if (string.IsNullOrWhiteSpace(bankCode) || bankCode.Length > 10)
            throw new DomainException("Bank code is required and must be max 10 characters.");

        if (string.IsNullOrWhiteSpace(accountNumber) || accountNumber.Length > 20)
            throw new DomainException("Account number is required and must be max 20 characters.");

        Alias = alias;
        BankCode = bankCode;
        AccountNumber = accountNumber;
        Currency = currency;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
