using BankDemo.Application.Interfaces.Repositories;
using BankDemo.Domain.Entities;
using BankDemo.Domain.Enums;
using BankDemo.Infrastructure.Data.Dapper;
using Dapper;

namespace BankDemo.Infrastructure.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    public TransferRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Transfer?> GetByCustomerAndRequestIdAsync(
        string customerId,
        string clientRequestId)
    {
        const string sql = """
            SELECT TOP 1 *
            FROM transfers
            WHERE customer_id = @CustomerId
              AND client_request_id = @ClientRequestId
        """;

        using var connection = _connectionFactory.CreateConnection();

        var result = await connection.QuerySingleOrDefaultAsync<dynamic>(
            sql,
            new { CustomerId = customerId, ClientRequestId = clientRequestId });

        if (result == null)
            return null;

        return new Transfer(
            result.id,
            result.customer_id,
            result.from_account_id,
            result.beneficiary_id,
            result.amount,
            Enum.Parse<Currency>(result.currency),
            result.description,
            result.client_request_id,
            result.created_at
        );
    }

    public async Task<decimal> GetDailyTotalAsync(
        string fromAccountId,
        Currency currency,
        DateTime date)
    {
        const string sql = """
            SELECT ISNULL(SUM(amount), 0)
            FROM transfers
            WHERE from_account_id = @AccountId
              AND currency = @Currency
              AND CAST(created_at AS DATE) = @Date
        """;

        using var connection = _connectionFactory.CreateConnection();

        return await connection.ExecuteScalarAsync<decimal>(
            sql,
            new
            {
                AccountId = fromAccountId,
                Currency = currency.ToString(),
                Date = date
            });
    }

    public async Task AddAsync(Transfer transfer)
    {
        const string sql = """
            INSERT INTO transfers (
                id,
                customer_id,
                from_account_id,
                beneficiary_id,
                amount,
                currency,
                description,
                status,
                client_request_id,
                created_at
            )
            VALUES (
                @Id,
                @CustomerId,
                @FromAccountId,
                @BeneficiaryId,
                @Amount,
                @Currency,
                @Description,
                @Status,
                @ClientRequestId,
                @CreatedAt
            )
        """;

        using var connection = _connectionFactory.CreateConnection();

        await connection.ExecuteAsync(sql, new
        {
            transfer.Id,
            transfer.CustomerId,
            transfer.FromAccountId,
            transfer.BeneficiaryId,
            transfer.Amount,
            Currency = transfer.Currency.ToString(),
            Status = transfer.Status.ToString(),
            transfer.Description,
            transfer.ClientRequestId,
            transfer.CreatedAt
        });
    }
}
