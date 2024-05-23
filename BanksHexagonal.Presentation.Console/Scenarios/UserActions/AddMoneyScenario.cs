using BanksHexagonal.Application.InputPort;
using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class AddMoneyScenario : IScenario
{
    private ICashMachineService _service;

    public AddMoneyScenario(ICashMachineService service)
    {
        _service = service;
    }

    public string Name => "Add money";

    public void Run()
    {
        int amount = AnsiConsole.Ask<int>("Enter the sum");
        _service.AddMoney(amount);
    }
}