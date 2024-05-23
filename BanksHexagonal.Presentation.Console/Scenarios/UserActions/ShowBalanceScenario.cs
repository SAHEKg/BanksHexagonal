using BanksHexagonal.Application.InputPort;

namespace BanksHexagonal.Presentation.Console;

public class ShowBalanceScenario : IScenario
{
    private ICashMachineService _service;

    public ShowBalanceScenario(ICashMachineService service)
    {
        _service = service;
    }

    public string Name => "Show Balance";

    public void Run()
    {
        _service.ShowBalance();
    }
}