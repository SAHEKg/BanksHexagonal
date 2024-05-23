using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class LoginAdminScenario : IScenario
{
    private IAdminService _service;

    public LoginAdminScenario(IAdminService service)
    {
        _service = service;
    }

    public string Name => "Login as admin";

    public void Run()
    {
        string password = AnsiConsole.Ask<string>("Enter system password");
        _service.Login(password);
    }
}