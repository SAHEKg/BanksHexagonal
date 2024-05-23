using System.Diagnostics.CodeAnalysis;
using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Presentation.Console;

public class ShowBalanceScenarioProvider : IScenarioProvider
{
    private ICashMachineService _service;
    private ICurrentUserService _currentUser;

    public ShowBalanceScenarioProvider(ICashMachineService service, ICurrentUserService currentUser)
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

        scenario = new ShowBalanceScenario(_service);
        return true;
    }
}