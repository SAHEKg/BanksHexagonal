namespace BanksHexagonal.Application.InputPort;

public interface ICashMachineService
{
    void ShowBalance();

    void AddMoney(int amount);

    void TakeMoney(int amount);
}