using BanksHexagonal.Application.InputPort;

namespace BanksHexagonal.Presentation.Console.Logout;

public class LogoutScenario : IScenario
{
    private IAccountHolderService _service;

    public LogoutScenario(IAccountHolderService service)
    {
        _service = service;
    }

    public string Name => "Log out";

    public void Run()
    {
        _service.Logout();
    }
}