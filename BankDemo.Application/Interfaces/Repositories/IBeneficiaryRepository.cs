using BankDemo.Domain.Entities;

namespace BankDemo.Application.Interfaces.Repositories;

public interface IBeneficiaryRepository
{
    Task<Beneficiary?> GetByIdAsync(Guid beneficiaryId);
}
