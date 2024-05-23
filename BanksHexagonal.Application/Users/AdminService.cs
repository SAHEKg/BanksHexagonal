using BanksHexagonal.Application.InputPort;
using BanksHexagonal.Application.Models;
using BanksHexagonal.Application.OutputPort;

namespace BanksHexagonal.Application;

public class AdminService : IAdminService
{
    private CurrentUserManager _currentUserManager;
    private IStringPrinter _printer;
    private IAccountsRepository _repository;
    private string _password;

    public AdminService(string password, CurrentUserManager currentUserManager, IStringPrinter printer, IAccountsRepository repository)
    {
        _currentUserManager = currentUserManager;
        _printer = printer;
        _repository = repository;
        _password = password;
    }

    public void Login(string password)
    {
        if (password != _password)
        {
            _printer.Display("Wrong system password");
            return;
        }

        _currentUserManager.CurrentUser = new User.Admin();
    }

    public void Logout()
    {
        _currentUserManager.CurrentUser = null;
    }

    public void ChangePassword(string newPassword)
    {
        _password = newPassword;
        _printer.Display("System password changed");
    }

    public void AddAccount(int number, int pin)
    {
        User.AccountHolder? accountHolder = _repository.FindAccountByNumber(number).Result;

        if (accountHolder is not null)
        {
            _printer.Display("User already exists");
            return;
        }

        _repository.AddAccount(number, pin);
    }
}