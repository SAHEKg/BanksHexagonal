using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BanksHexagonal.Infrastructure.DataAccess;

public static class ServiceScopeDataAccessExtension
{
    public static void UseInfrastructureDataAccess(this IServiceScope scope)
    {
        scope.UsePlatformMigrationsAsync(default).GetAwaiter().GetResult();
    }
}