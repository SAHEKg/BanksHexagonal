using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class StringPrinter : IStringPrinter
{
    public void Display(string message)
    {
        System.Console.WriteLine($"{message}\n\n");
        AnsiConsole.Ask<string>("Type something to continue...");
    }
}