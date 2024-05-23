using System.Diagnostics.CodeAnalysis;

namespace BanksHexagonal.Presentation.Console;

public interface IScenarioProvider
{
    bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario);
}