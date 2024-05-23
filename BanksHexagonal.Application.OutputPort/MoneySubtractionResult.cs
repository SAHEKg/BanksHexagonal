namespace BanksHexagonal.Application.OutputPort;

public record MoneySubtractionResult
{
    private MoneySubtractionResult() { }

    public sealed record Success : MoneySubtractionResult;

    public sealed record NotEnoughMoney : MoneySubtractionResult;
}