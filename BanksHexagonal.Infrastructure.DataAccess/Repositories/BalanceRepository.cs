using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;
using Npgsql;

namespace BanksHexagonal.Infrastructure.DataAccess;

public class BalanceRepository : IBalanceRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;
    private readonly ITransactionLogRepository _logger;

    public BalanceRepository(IPostgresConnectionProvider connectionProvider, ITransactionLogRepository logger)
    {
        _connectionProvider = connectionProvider;
        _logger = logger;
    }

    public async Task<int> FindBalanceByNumber(int number)
    {
        const string sql = """
        SELECT account_number, balance
        FROM account_balance
        WHERE account_number = :number;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("number", number);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            return reader.GetInt32(1);

        throw new InvalidDataException($"no account number {number}");
    }

    public async void IncreaseBalanceByNumber(int number, int amount)
    {
        if (number <= 0)
            return;

        const string sql = """
        UPDATE account_balance
        SET balance = balance + :amount
        WHERE account_number = :number;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command
            .AddParameter("number", number)
            .AddParameter("amount", amount);

        await command.ExecuteNonQueryAsync();

        _logger.AddTransaction(TransactionType.Add, number, amount);
    }

    public async Task<MoneySubtractionResult> DecreaseBalanceByNumber(int number, int amount)
    {
        int current = await FindBalanceByNumber(number);

        if (current - amount < 0)
            return new MoneySubtractionResult.NotEnoughMoney();

        await SubtractMoney(number, amount);
        _logger.AddTransaction(TransactionType.Take, number, amount);

        return new MoneySubtractionResult.Success();
    }

    private async Task SubtractMoney(int number, int amount)
    {
        const string sql = """
        UPDATE account_balance
        SET balance = balance - :amount
        WHERE account_number = :number;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command
            .AddParameter("number", number)
            .AddParameter("amount", amount);

        await command.ExecuteNonQueryAsync();
    }
}