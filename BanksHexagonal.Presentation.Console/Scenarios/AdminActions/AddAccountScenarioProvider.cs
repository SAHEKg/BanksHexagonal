using System.Diagnostics.CodeAnalysis;
using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Presentation.Console;

public class AddAccountScenarioProvider : IScenarioProvider
{
    private IAdminService _service;
    private ICurrentUserService _currentUser;

    public AddAccountScenarioProvider(IAdminService service, ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.CurrentUser is not User.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new AddAccountScenario(_service);
        return true;
    }
}