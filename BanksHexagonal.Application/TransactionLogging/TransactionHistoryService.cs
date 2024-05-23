using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;

namespace BanksHexagonal.Application;

public class TransactionHistoryService : ITransactionHistoryService
{
    private ITransactionLogRepository _repository;
    private ICurrentUserService _currentUser;
    private ITransactionHistoryTablePrinter _printer;

    public TransactionHistoryService(
        ITransactionLogRepository repository,
        ICurrentUserService currentUser,
        ITransactionHistoryTablePrinter printer)
    {
        _repository = repository;
        _currentUser = currentUser;
        _printer = printer;
    }

    public void ShowHistory()
    {
        if (_currentUser.CurrentUser is not User.AccountHolder accountHolder)
            return;

        _printer.Display(_repository.GetHistoryForAccount(accountHolder.AccountNumber).Result);
    }
}