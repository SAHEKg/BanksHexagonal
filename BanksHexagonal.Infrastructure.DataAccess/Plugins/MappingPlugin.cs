using Itmo.Dev.Platform.Postgres.Plugins;
using BanksHexagonal.Application.Models;
using Npgsql;

namespace BanksHexagonal.Infrastructure.DataAccess;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<TransactionType>();
    }
}