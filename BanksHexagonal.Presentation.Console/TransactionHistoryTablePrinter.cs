using System.Globalization;
using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class TransactionHistoryTablePrinter : ITransactionHistoryTablePrinter
{
    public void Display(IEnumerable<TransactionHistory> history)
    {
        System.Console.WriteLine("Transaction history");

        var table = new Table();
        table
            .AddColumn("Account number")
            .AddColumn("Transaction type")
            .AddColumn("Sum");

        foreach (TransactionHistory transaction in history)
        {
            table.AddRow(
                transaction.AccountNumber.ToString(new CultureInfo("eng")),
                transaction.Type.ToString(),
                transaction.Amount.ToString(new CultureInfo("eng")));
        }

        AnsiConsole.Write(table);
        System.Console.WriteLine(string.Empty);
        AnsiConsole.Ask<string>("Type something to continue...");
    }
}