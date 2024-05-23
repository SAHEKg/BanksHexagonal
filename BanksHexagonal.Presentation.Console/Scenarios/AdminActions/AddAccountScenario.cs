using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class AddAccountScenario : IScenario
{
    private IAdminService _service;

    public AddAccountScenario(IAdminService service)
    {
        _service = service;
    }

    public string Name => "Add account";

    public void Run()
    {
        int accountNumber = AnsiConsole.Ask<int>("Enter new account number");
        int pin = AnsiConsole.Ask<int>("Enter new pin");

        _service.AddAccount(accountNumber, pin);
    }
}