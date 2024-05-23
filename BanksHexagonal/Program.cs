using BanksHexagonal.Application;
using BanksHexagonal.Presentation.Console;
using BanksHexagonal.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace BanksHexagonal;

public static class Program
{
    public static void Main()
    {
        IServiceCollection collection = new ServiceCollection();

        collection
            .AddApplication()
            .AddInfrastructureDataAccess(configuration =>
            {
                configuration.Host = "localhost";
                configuration.Port = 6432;
                configuration.Username = "postgres";
                configuration.Password = "postgres";
                configuration.Database = "postgres";
                configuration.SslMode = "Prefer";
            })
            .AddConsoleInterface();

        IServiceProvider provider = collection.BuildServiceProvider();
        using IServiceScope scope = provider.CreateScope();

        scope.UseInfrastructureDataAccess();

        ScenarioRunner runner = scope.ServiceProvider.GetRequiredService<ScenarioRunner>();

        while (true)
        {
            runner.Run();
            Console.Clear();
        }
    }
}
