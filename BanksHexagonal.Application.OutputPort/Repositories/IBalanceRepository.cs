namespace BanksHexagonal.Application.OutputPort;

public interface IBalanceRepository
{
    Task<int> FindBalanceByNumber(int number);

    void IncreaseBalanceByNumber(int number, int amount);

    Task<MoneySubtractionResult> DecreaseBalanceByNumber(int number, int amount);
}