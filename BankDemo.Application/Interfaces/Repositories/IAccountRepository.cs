using System.Security.Principal;
using BankDemo.Domain.Entities;

namespace BankDemo.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(string accountId);
}
