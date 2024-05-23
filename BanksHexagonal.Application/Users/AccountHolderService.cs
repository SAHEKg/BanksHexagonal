using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;

namespace BanksHexagonal.Application;

public class AccountHolderService : IAccountHolderService
{
    private IAccountsRepository _repository;
    private CurrentUserManager _currentUserManager;
    private IStringPrinter _printer;

    public AccountHolderService(CurrentUserManager currentUserManager, IAccountsRepository repository, IStringPrinter printer)
    {
        _currentUserManager = currentUserManager;
        _repository = repository;
        _printer = printer;
    }

    public void Login(int number, int pin)
    {
        User.AccountHolder? user = _repository.FindAccountByNumber(number).Result;
        if (user is null)
        {
            _printer.Display("User not found");
        }
        else if (user.Pin != pin)
        {
            _printer.Display("Wrong password");
        }

        _currentUserManager.CurrentUser = user;
    }

    public void Logout()
    {
        _currentUserManager.CurrentUser = null;
    }
}
