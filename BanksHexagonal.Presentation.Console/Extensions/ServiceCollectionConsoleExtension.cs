using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Presentation.Console.Logout;
using Microsoft.Extensions.DependencyInjection;

namespace BanksHexagonal.Presentation.Console;

public static class ServiceCollectionConsoleExtension
{
    public static IServiceCollection AddConsoleInterface(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();

        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AddMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, TakeMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowHistoryScenarioProvider>();

        collection.AddScoped<IScenarioProvider, AddAccountScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ChangePasswordScenarioProvider>();

        collection.AddScoped<IScenarioProvider, LogoutScenarioProvider>();
        collection.AddScoped<IStringPrinter, StringPrinter>();
        collection.AddScoped<ITransactionHistoryTablePrinter, TransactionHistoryTablePrinter>();

        return collection;
    }
}