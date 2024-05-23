using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class LoginUserScenario : IScenario
{
    private IAccountHolderService _service;

    public LoginUserScenario(IAccountHolderService service)
    {
        _service = service;
    }

    public string Name => "Login as user";

    public void Run()
    {
        int accountNumber = AnsiConsole.Ask<int>("Enter your account number");
        int pin = AnsiConsole.Ask<int>("Enter your pin");

        _service.Login(accountNumber, pin);
    }
}