using BankDemo.Application.Interfaces.Repositories;
using BankDemo.Domain.Entities;
using BankDemo.Domain.Enums;
using BankDemo.Domain.Exceptions;

namespace BankDemo.Application.Transfers.CreateTransfer;

public class CreateTransferHandler
{
    private readonly IAccountRepository _accountRepository;
    private readonly IBeneficiaryRepository _beneficiaryRepository;
    private readonly ITransferRepository _transferRepository;

    public CreateTransferHandler(
        IAccountRepository accountRepository,
        IBeneficiaryRepository beneficiaryRepository,
        ITransferRepository transferRepository)
    {
        _accountRepository = accountRepository;
        _beneficiaryRepository = beneficiaryRepository;
        _transferRepository = transferRepository;
    }

    public async Task<Transfer> HandleAsync(CreateTransferCommand command)
    {
        // 1. Idempotency
        var existingTransfer =
            await _transferRepository.GetByCustomerAndRequestIdAsync(
                command.CustomerId,
                command.ClientRequestId);

        if (existingTransfer != null)
            return existingTransfer;

        // 2. Account validation
        var account = await _accountRepository.GetByIdAsync(command.FromAccountId);
        if (account == null || account.CustomerId != command.CustomerId)
            throw new DomainException("Source account not found for customer.");

        // 3. Beneficiary validation
        var beneficiary = await _beneficiaryRepository.GetByIdAsync(command.BeneficiaryId);
        if (beneficiary == null ||
            beneficiary.CustomerId != command.CustomerId ||
            !beneficiary.IsActive)
            throw new DomainException("Beneficiary not found or inactive.");

        // 4. Daily limit validation
        var today = DateTime.UtcNow.Date;
        var dailyTotal = await _transferRepository.GetDailyTotalAsync(
            command.FromAccountId,
            command.Currency,
            today);

        var limit = command.Currency switch
        {
            Currency.BOB => 20000m,
            Currency.USD => 3000m,
            _ => throw new DomainException("Unsupported currency.")
        };

        if (dailyTotal + command.Amount > limit)
            throw new DomainException("Daily transfer limit exceeded.");

        // 5. Create transfer
        var transfer = new Transfer(
            Guid.NewGuid(),
            command.CustomerId,
            command.FromAccountId,
            command.BeneficiaryId,
            command.Amount,
            command.Currency,
            command.Description,
            command.ClientRequestId,
            DateTime.UtcNow);

        // 6. Persist
        await _transferRepository.AddAsync(transfer);

        return transfer;
    }
}
