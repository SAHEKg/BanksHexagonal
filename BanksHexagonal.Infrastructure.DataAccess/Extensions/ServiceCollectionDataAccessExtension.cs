using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using BanksHexagonal.Application.OutputPort;
using Microsoft.Extensions.DependencyInjection;

namespace BanksHexagonal.Infrastructure.DataAccess;

public static class ServiceCollectionDataAccessExtension
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionDataAccessExtension).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddScoped<IAccountsRepository, AccountsRepository>();
        collection.AddScoped<IBalanceRepository, BalanceRepository>();
        collection.AddScoped<ITransactionLogRepository, TransactionLogRepository>();

        return collection;
    }
}