using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class ChangePasswordScenario : IScenario
{
    private IAdminService _service;

    public ChangePasswordScenario(IAdminService service)
    {
        _service = service;
    }

    public string Name => "Change system password";

    public void Run()
    {
        string newPassword = AnsiConsole.Ask<string>("Enter new password");
        _service.ChangePassword(newPassword);
    }
}