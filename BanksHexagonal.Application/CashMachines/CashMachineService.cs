using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;

namespace BanksHexagonal.Application.InputPort;

public class CashMachineService : ICashMachineService
{
    private IBalanceRepository _repository;
    private ICurrentUserService _currentUser;
    private IStringPrinter _printer;

    public CashMachineService(ICurrentUserService currentUser, IBalanceRepository repository, IStringPrinter printer)
    {
        _currentUser = currentUser;
        _repository = repository;
        _printer = printer;
    }

    public void ShowBalance()
    {
        if (_currentUser.CurrentUser is not User.AccountHolder accountHolder)
            return;

        int balance = _repository.FindBalanceByNumber(accountHolder.AccountNumber).Result;
        _printer.Display($"Current balance: {balance}");
    }

    public void AddMoney(int amount)
    {
        if (_currentUser.CurrentUser is not User.AccountHolder accountHolder)
            return;

        _repository.IncreaseBalanceByNumber(accountHolder.AccountNumber, amount);
        _printer.Display($"Account balance increased by {amount}");
    }

    public void TakeMoney(int amount)
    {
        if (_currentUser.CurrentUser is not User.AccountHolder accountHolder)
            return;

        MoneySubtractionResult result = _repository.DecreaseBalanceByNumber(accountHolder.AccountNumber, amount).Result;

        switch (result)
        {
            case MoneySubtractionResult.NotEnoughMoney:
                _printer.Display("Balance not sufficient");
                break;
            case MoneySubtractionResult.Success:
                _printer.Display($"Account balance decreased by {amount}");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(amount));
        }
    }
}