using System.Diagnostics.CodeAnalysis;
using BanksHexagonal.Application.InputPort;

namespace BanksHexagonal.Presentation.Console.Logout;

public class LogoutScenarioProvider : IScenarioProvider
{
    private IAccountHolderService _service;
    private ICurrentUserService _currentUser;

    public LogoutScenarioProvider(IAccountHolderService service, ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.CurrentUser is null)
        {
            scenario = null;
            return false;
        }

        scenario = new LogoutScenario(_service);
        return true;
    }
}