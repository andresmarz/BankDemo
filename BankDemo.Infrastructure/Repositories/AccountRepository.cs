using BankDemo.Application.Interfaces.Repositories;
using BankDemo.Domain.Entities;
using BankDemo.Domain.Enums;
using BankDemo.Infrastructure.Data.Dapper;
using Dapper;

namespace BankDemo.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public AccountRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Account?> GetByIdAsync(string accountId)
    {
        const string sql = """
            SELECT
                id,
                customer_id AS CustomerId,
                currency,
                available_balance AS AvailableBalance
            FROM accounts
            WHERE id = @AccountId
        """;

        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<dynamic>(
            sql,
            new { AccountId = accountId });

        if (result == null)
            return null;

        return new Account(
            result.id,
            result.CustomerId,
            Enum.Parse<Currency>(result.currency),
            result.AvailableBalance
        );
    }
}
