using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;
using Npgsql;

namespace BanksHexagonal.Infrastructure.DataAccess;

public class AccountsRepository : IAccountsRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public AccountsRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<User.AccountHolder?> FindAccountByNumber(int number)
    {
        const string sql = """
        SELECT account_number, pin
        FROM accounts
        WHERE account_number = :number;
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("number", number);

        await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            return new User.AccountHolder(reader.GetInt32(0), reader.GetInt32(1));

        return null;
    }

    public async void AddAccount(int number, int pin)
    {
        const string sql = """
        INSERT INTO accounts
        VALUES (:number, :pin);
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command
            .AddParameter("number", number)
            .AddParameter("pin", pin);

        await command.ExecuteNonQueryAsync();

        AddEmptyBalance(number);
    }

    private async void AddEmptyBalance(int number)
    {
        const string sql = """
        INSERT INTO account_balance
        VALUES (:number, 0);
        """;

        NpgsqlConnection connection = await _connectionProvider
            .GetConnectionAsync(default);

        await using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("number", number);

        await command.ExecuteNonQueryAsync();
    }
}