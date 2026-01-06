using BankDemo.Application.Interfaces.Repositories;
using BankDemo.Domain.Entities;
using BankDemo.Domain.Enums;
using BankDemo.Infrastructure.Data.Dapper;
using Dapper;

namespace BankDemo.Infrastructure.Repositories;

public class BeneficiaryRepository : IBeneficiaryRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public BeneficiaryRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Beneficiary?> GetByIdAsync(Guid beneficiaryId)
    {
        const string sql = """
            SELECT
                id,
                customer_id AS CustomerId,
                alias,
                bank_code AS BankCode,
                account_number AS AccountNumber,
                currency,
                is_active AS IsActive
            FROM beneficiaries
            WHERE id = @Id
        """;

        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<dynamic>(
            sql,
            new { Id = beneficiaryId });

        if (result == null)
            return null;

        return new Beneficiary(
            result.id,
            result.CustomerId,
            result.alias,
            result.BankCode,
            result.AccountNumber,
            Enum.Parse<Currency>(result.currency),
            result.IsActive
        );
    }
}
