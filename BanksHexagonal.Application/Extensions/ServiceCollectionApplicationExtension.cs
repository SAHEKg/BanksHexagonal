using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.OutputPort;
using Microsoft.Extensions.DependencyInjection;

namespace BanksHexagonal.Application;

public static class ServiceCollectionApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<CurrentUserManager>();
        collection.AddScoped<ICurrentUserService>(
            provider => provider.GetRequiredService<CurrentUserManager>());

        collection.AddScoped<IAccountHolderService, AccountHolderService>();

        collection.AddScoped<IAdminService>(
            provider => new AdminService(
                "1234",
                provider.GetRequiredService<CurrentUserManager>(),
                provider.GetRequiredService<IStringPrinter>(),
                provider.GetRequiredService<IAccountsRepository>()));

        collection.AddScoped<ICashMachineService, CashMachineService>();
        collection.AddScoped<ITransactionHistoryService, TransactionHistoryService>();

        return collection;
    }
}