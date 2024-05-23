using BanksHexagonal.Application.Models;

namespace BanksHexagonal.Application.OutputPort;

public interface IAccountsRepository
{
    Task<User.AccountHolder?> FindAccountByNumber(int number);

    void AddAccount(int number, int pin);
}