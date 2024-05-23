using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;
using Npgsql;

namespace BanksHexagonal.Infrastructure.DataAccess;

public class TransactionLogRepository : ITransactionLogRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionLogRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<IEnumerable<TransactionHistory>> GetHistoryForAccount(int number)
    {
        const string sql = """
        SELECT account_number, transaction_type, sum
        FROM transaction_history
        WHERE account_number = :number;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("number", number);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var result = new List<TransactionHistory>();
        while (await reader.ReadAsync())
        {
            result.Add(new TransactionHistory(
                reader.GetInt32(0),
                await reader.GetFieldValueAsync<TransactionType>(1),
                reader.GetInt32(2)));
        }

        return result;
    }

    public async void AddTransaction(TransactionType type, int number, int amount)
    {
        const string sql = """
        INSERT INTO transaction_history (
                                         account_number,
                                         transaction_type,
                                         sum)
        VALUES (:number, :type, :amount)
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command
            .AddParameter("number", number)
            .AddParameter("type", type)
            .AddParameter("amount", amount);

        await command.ExecuteNonQueryAsync();
    }
}