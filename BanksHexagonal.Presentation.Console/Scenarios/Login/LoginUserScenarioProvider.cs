using System.Diagnostics.CodeAnalysis;
using BanksHexagonal.Application.InputPort;

namespace BanksHexagonal.Presentation.Console;

public class LoginUserScenarioProvider : IScenarioProvider
{
    private IAccountHolderService _service;
    private ICurrentUserService _currentUser;

    public LoginUserScenarioProvider(IAccountHolderService service, ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.CurrentUser is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginUserScenario(_service);
        return true;
    }
}
