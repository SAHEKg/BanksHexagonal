using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class TakeMoneyScenario : IScenario
{
    private ICashMachineService _service;

    public TakeMoneyScenario(ICashMachineService service)
    {
        _service = service;
    }

    public string Name => "Take money";

    public void Run()
    {
        int amount = AnsiConsole.Ask<int>("Enter the sum");
        _service.TakeMoney(amount);
    }
}