using BanksHexagonal.Application.InputPort;

namespace BanksHexagonal.Presentation.Console;

public class ShowHistoryScenario : IScenario
{
    private ITransactionHistoryService _service;

    public ShowHistoryScenario(ITransactionHistoryService service)
    {
        _service = service;
    }

    public string Name => "Show transaction history";

    public void Run()
    {
        _service.ShowHistory();
    }
}