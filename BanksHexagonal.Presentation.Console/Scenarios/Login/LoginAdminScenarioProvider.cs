using System.Diagnostics.CodeAnalysis;
using BanksHexagonal.Application.InputPort;

namespace BanksHexagonal.Presentation.Console;

public class LoginAdminScenarioProvider : IScenarioProvider
{
    private IAdminService _service;
    private ICurrentUserService _currentUser;

    public LoginAdminScenarioProvider(IAdminService service, ICurrentUserService currentUser)
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

        scenario = new LoginAdminScenario(_service);
        return true;
    }
}