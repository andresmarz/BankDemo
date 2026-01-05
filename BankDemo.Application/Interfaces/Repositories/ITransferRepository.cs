using BankDemo.Domain.Entities;
using BankDemo.Domain.Enums;

namespace BankDemo.Application.Interfaces.Repositories;

public interface ITransferRepository
{
    Task<Transfer?> GetByCustomerAndRequestIdAsync(string customerId, string clientRequestId);

    Task<decimal> GetDailyTotalAsync(
        string fromAccountId,
        Currency currency,
        DateTime date);

    Task AddAsync(Transfer transfer);
}
