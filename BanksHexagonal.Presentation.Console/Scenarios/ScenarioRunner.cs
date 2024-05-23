using Spectre.Console;

namespace BanksHexagonal.Presentation.Console;

public class ScenarioRunner
{
    private IEnumerable<IScenarioProvider> _scenarioProviders;

    public ScenarioRunner(IEnumerable<IScenarioProvider> scenarioProviders)
    {
        _scenarioProviders = scenarioProviders;
    }

    public void Run()
    {
        IEnumerable<IScenario> scenarios = GetScenarios();

        SelectionPrompt<IScenario> prompt =
            new SelectionPrompt<IScenario>()
                .Title("Select action")
                .AddChoices(scenarios)
                .UseConverter(s => s.Name);

        IScenario scenario = AnsiConsole.Prompt(prompt);
        scenario.Run();
    }

    private IEnumerable<IScenario> GetScenarios()
    {
        foreach (IScenarioProvider provider in _scenarioProviders)
        {
            if (provider.TryGetScenario(out IScenario? scenario))
                yield return scenario;
        }
    }
}