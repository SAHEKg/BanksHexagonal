using System.Diagnostics.CodeAnalysis;
using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Presentation.Console;

public class TakeMoneyScenarioProvider : IScenarioProvider
{
    private ICashMachineService _service;
    private ICurrentUserService _currentUser;

    public TakeMoneyScenarioProvider(ICashMachineService service, ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.CurrentUser is not User.AccountHolder)
        {
            scenario = null;
            return false;
        }

        scenario = new TakeMoneyScenario(_service);
        return true;
    }
}